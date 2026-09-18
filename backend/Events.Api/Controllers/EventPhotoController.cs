using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.DTOs;
using Events.Application.Queries;
using Events.Application.Commands;
using MediatR;
using Events.Application.Mappers;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventPhotoController : ControllerBase
{
    private readonly ISender _sender;
    private readonly CommandMapper _commandMapper;

    public EventPhotoController(ISender sender, CommandMapper commandMapper)
    {
        _sender = sender;
        _commandMapper = commandMapper;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventPhotoResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var query = new GetEventPhotoByIdQuery(id);
        var eventPhoto = await _sender.Send(query, ct);

        return Ok(eventPhoto);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<EventPhotoResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetEventPhotosQuery();
        var eventPhotos = await _sender.Send(query, ct);

        return Ok(eventPhotos);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<EventPhotoResponseDTO>> Create([FromBody] EventPhotoCreateDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }

    [HttpPatch("{id:Guid}")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(typeof(EventPhotoResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventPhotoResponseDTO>> Update(Guid id, [FromBody] EventPhotoUpdateDTO dto, CancellationToken ct = default)
    {
        var command = _commandMapper.MapToCommand(id, dto);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> DeleteEventPhoto(Guid id, CancellationToken ct = default)
    {
        var command = new DeleteEventPhotoCommand(id);
        await _sender.Send(command, ct);

        return Ok($"Slika sa ID-jem {id} je uspesno izbrisana");
    }
}
