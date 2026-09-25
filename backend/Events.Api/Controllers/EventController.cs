using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.DTOs;
using Events.Application.Queries;
using Events.Application.Mappers;
using MediatR;
using Events.Application.Commands;
using Events.API.Requests;
using Events.Application.Storage;
using Swashbuckle.AspNetCore.Annotations;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly ISender _sender;
    private readonly CommandMapper _commandMapper;

    public EventController(
        ISender sender,
        CommandMapper commandMapper)
    {
        _sender = sender;
        _commandMapper = commandMapper;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken ct)
    {
        var query = new GetEventByIdQuery(id);
        var result = await _sender.Send(query, ct);

        return Ok(result);
    }

    [HttpGet("all")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<EventResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetEventsQuery();

        var result = await _sender.Send(query, ct);

        return Ok(result);
    }
    
    [HttpPost("create")]
    [Authorize(Roles = "Organizer, Admin")]
    [Consumes("multipart/form-data")]
    [SwaggerOperation(OperationId = "CreateEvent")]
    [ProducesResponseType(typeof(EventResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<EventResponseDTO>> Create([FromForm] CreateEventRequest request, CancellationToken ct)
    {
        //extract these later
        var dto = new EventCreateDTO(
            request.Title,
            request.Description,
            request.Country,
            request.City,
            request.Address,
            request.VenueName,
            request.StartOfEvent,
            request.EndOfEvent
        );

        var file = new FileUpload(
            request.MainImage.OpenReadStream(),
            request.MainImage.FileName,
            request.MainImage.ContentType,
            request.MainImage.Length
        );

        var command = _commandMapper.MapToCommand(dto, file);

        var result = await _sender.Send(command, ct);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPatch("{id:Guid}")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(typeof(TicketTypeResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EventResponseDTO>> Update(Guid id, [FromBody] EventUpdateDTO dto, CancellationToken ct = default)
    {
        var command = _commandMapper.MapToCommand(id, dto);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> Delete(Guid id, CancellationToken ct = default)
    {
        var command = new DeleteEventCommand(id);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }
}