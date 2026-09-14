using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Api.Services;
using Events.Api.DTOs;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var eventItem = await _eventService.GetByIdAsync(id);

        if (eventItem == null)
        {
            return NotFound(new { message = $"Dogadjaj sa ID-jem {id} nije pronadjen." });
        }

        return Ok(eventItem);
    }

    [HttpGet("all")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<EventResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync()
    {
        var events = await _eventService.GetAllAsync();
        if (!events.Any())
            return NotFound(new { message = "Nema dogadjaja u bazi." });

        return Ok(events);
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
        try
        {
            var result = await _eventService.CreateAsync(dto, ct);
            
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