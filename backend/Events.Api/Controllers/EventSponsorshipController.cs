using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.Services;
using Events.Application.DTOs;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventSponsorshipController : ControllerBase
{
    private readonly IEventSponsorshipService _eventsponsorshipService;

    public EventSponsorshipController(IEventSponsorshipService eventsponsorshipService)
    {
        _eventsponsorshipService = eventsponsorshipService;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventSponsorshipResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var eventsponsorship = await _eventsponsorshipService.GetByIdAsync(id);

        if (eventsponsorship == null)
        {
            return NotFound(new { message = $"Sponzorstvo sa ID-jem {id} nije pronadjeno." });
        }

        return Ok(eventsponsorship);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<EventSponsorshipResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync()
    {
        var eventsponsorships = await _eventsponsorshipService.GetAllAsync();
        if (!eventsponsorships.Any())
            return NotFound(new { message = "Nema sponzorstva u bazi." });

        return Ok(eventsponsorships);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<EventSponsorshipResponseDTO>> Create([FromBody] EventSponsorshipCreateDTO dto, CancellationToken ct)
    {
        var result = await _eventsponsorshipService.CreateAsync(dto, ct);
            
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }
}