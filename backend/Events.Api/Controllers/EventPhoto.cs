using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.Services;
using Events.Application.DTOs;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventPhotoController : ControllerBase
{
    private readonly IEventPhotoService _eventphotoService;

    public EventPhotoController(IEventPhotoService eventphotoService)
    {
        _eventphotoService = eventphotoService;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventPhotoResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var eventphoto = await _eventphotoService.GetByIdAsync(id);

        if (eventphoto == null)
        {
            return NotFound(new { message = $"Slika sa ID-jem {id} nije pronadjena." });
        }

        return Ok(eventphoto);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<EventPhotoResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync()
    {
        var eventphotos = await _eventphotoService.GetAllAsync();
        if (!eventphotos.Any())
            return NotFound(new { message = "Nema slika dogadjaja u bazi." });

        return Ok(eventphotos);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<EventPhotoResponseDTO>> Create([FromBody] EventPhotoCreateDTO dto, CancellationToken ct)
    {
        var result = await _eventphotoService.CreateAsync(dto, ct);
            
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }
}