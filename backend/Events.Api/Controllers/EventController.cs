using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.DTOs;
using Events.Application.Queries;
using Events.Application.Mappers;
using MediatR;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly ISender _sender;
    private readonly CQMapper _cqMapper;

    public EventController(
        ISender sender,
        CQMapper cqMapper)
    {
        _sender = sender;
        _cqMapper = cqMapper;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var query = new GetEventByIdQuery(id);
        var result = await _sender.Send(query, ct);

        return Ok(result);
    }

    [HttpGet("all")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<EventResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetEventsQuery();

        var result = await _sender.Send(query, ct);

        return Ok(result);
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
        var result = await _sender.Send(command, ct);
        if(result is null)
            throw new ArgumentException("Zahtev za kreiranje dogadjaja nije uspesan.");

        return Ok(result);
    }
}