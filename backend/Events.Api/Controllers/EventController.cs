using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.Services;
using Events.Application.DTOs;
using Events.Domain.Exceptions;
using Events.Application.Messaging;
using Events.Application.Commands;
using Events.Application.Queries;
using Events.Application.Mappers;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly CQMapper _cqMapper;

    public EventController(
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        CQMapper cqMapper)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _cqMapper = cqMapper;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var query = new GetEventByIdQuery(id);
        var result = await _queryDispatcher.Send<GetEventByIdQuery, EventResponseDTO>(query, ct);

        if(!result.IsSuccess)
            throw new EventNotFoundException(id);

        return Ok(result.Value);
    }

    [HttpGet("all")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<EventResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetEventsQuery();

        var result = await _queryDispatcher.Send<GetEventsQuery, List<EventResponseDTO>>(query, ct);

        if(!result.IsSuccess)
            throw new EventsNotFoundException();

        return Ok(result.Value);
    }
    
    [HttpPost("create")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<EventResponseDTO>> Create([FromBody] EventCreateDTO dto, CancellationToken ct)
    {
        var command = _cqMapper.MapToCommand(dto);
        var result = await _commandDispatcher.Send<CreateEventCommand, EventResponseDTO>(command, ct);
        if(!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}