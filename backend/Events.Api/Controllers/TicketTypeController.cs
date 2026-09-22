using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.DTOs;
using Events.Application.Queries;
using Events.Application.Commands;
using Events.Application.Mappers;
using MediatR;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketTypeController : ControllerBase
{
    private readonly ISender _sender;
    private readonly CommandMapper _commandMapper;

    public TicketTypeController(ISender sender, CommandMapper commandMapper)
    {
        _sender = sender;
        _commandMapper = commandMapper;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TicketTypeResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var query = new GetTicketTypeByIdQuery(id);
        var ticketType = await _sender.Send(query, ct);

        return Ok(ticketType);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<TicketTypeResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetTicketTypesQuery();
        var ticketTypes = await _sender.Send(query, ct);

        return Ok(ticketTypes);
    }

    [HttpGet("event/{eventId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<TicketTypeResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEventIdAsync(Guid eventId, CancellationToken ct = default)
    {
        var query = new GetTicketTypesByEventIdQuery(eventId);
        var ticketTypes = await _sender.Send(query, ct);

        return Ok(ticketTypes);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(typeof(TicketTypeResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketTypeResponseDTO>> Create([FromBody] TicketTypeCreateDTO dto, CancellationToken ct = default)
    {
        var command = _commandMapper.MapToCommand(dto);
        var result = await _sender.Send(command, ct);

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id = result.Id },
            result);
    }

    [HttpPatch("{id:Guid}")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(typeof(TicketTypeResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketTypeResponseDTO>> Update(Guid id, [FromBody] TicketTypeUpdateDTO dto, CancellationToken ct = default)
    {
        var command = _commandMapper.MapToCommand(id, dto);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> Delete(Guid id, CancellationToken ct = default)
    {
        var command = new DeleteTicketTypeCommand(id);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }
}
