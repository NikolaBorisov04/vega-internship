using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.Services;
using Events.Application.DTOs;
using Events.Domain.Exceptions;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketTypeController : ControllerBase
{
    private readonly ITicketTypeService _ticketTypeService;

    public TicketTypeController(ITicketTypeService ticketTypeService, ICurrentUserService currentUserService)
    {
        _ticketTypeService = ticketTypeService;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TicketTypeResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var ticketType = await _ticketTypeService.GetByIdAsync(id);

        if (ticketType == null)
            throw new TicketTypeNotFoundException(id);

        return Ok(ticketType);
    }

    [HttpGet("all")]
    [Authorize(Roles ="Admin")]
    [ProducesResponseType(typeof(IEnumerable<TicketTypeResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync()
    {
        var ticketTypes = await _ticketTypeService.GetAllAsync();
        if (!ticketTypes.Any())
            return NotFound(new { message = "Nema tipova tiketa u bazi." });

        return Ok(ticketTypes);
    }

    [HttpGet("event/{eventId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<TicketTypeResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEventIdAsync(Guid eventId, CancellationToken ct)
    {
        var ticketTypes = await _ticketTypeService.GetByEventIdAsync(eventId, ct);

        if (!ticketTypes.Any())
            throw new NoTicketTypesForEventIdException(eventId);

        return Ok(ticketTypes);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(typeof(TicketTypeResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketTypeResponseDTO>> Create([FromBody] TicketTypeCreateDTO dto, CancellationToken ct)
    {
        var result = await _ticketTypeService.CreateAsync(dto, ct);

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id = result.Id },
            result);
    }
}