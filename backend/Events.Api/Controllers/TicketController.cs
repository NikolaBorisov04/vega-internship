using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.Services;
using Events.Application.DTOs;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ICurrentUserService _currentUserService;

    public TicketController(ITicketService ticketService, ICurrentUserService currentUserService)
    {
        _ticketService = ticketService;
        _currentUserService = currentUserService;
    }

    [HttpGet("{id:Guid}")]
    [Authorize]
    [ProducesResponseType(typeof(TicketResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound(new { message = $"Karta sa ID-jem {id} nije pronadjena." });
        }

        return Ok(ticket);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<TicketResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllAsync()
    {
        var tickets = await _ticketService.GetAllAsync();
        if (!tickets.Any())
            return NotFound(new { message = "Nema karata u bazi." });

        return Ok(tickets);
    }

    [HttpPost("create")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TicketResponseDTO>> Create([FromBody] TicketCreateDTO dto, CancellationToken ct)
    {
        try
        {
            var customerId = _currentUserService.UserId;

            var result = await _ticketService.CreateAsync(dto, customerId, ct);
            
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }
}