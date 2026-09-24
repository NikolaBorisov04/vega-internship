using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.DTOs;
using Events.Application.Queries;
using Events.Application.Commands;
using Events.Application.Mappers;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Events.API.Requests;
using Events.Application.Storage;

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var query = new GetTicketTypeByIdQuery(id);
        var ticketType = await _sender.Send(query, ct);

        return Ok(ticketType);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<TicketTypeResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetTicketTypesQuery();
        var ticketTypes = await _sender.Send(query, ct);

        return Ok(ticketTypes);
    }

    [HttpGet("event/{eventId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<TicketTypeResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEventIdAsync(Guid eventId, CancellationToken ct = default)
    {
        var query = new GetTicketTypesByEventIdQuery(eventId);
        var ticketTypes = await _sender.Send(query, ct);

        return Ok(ticketTypes);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Organizer, Admin")]
    [Consumes("multipart/form-data")]
    [SwaggerOperation(OperationId = "CreateTicketType")]
    [ProducesResponseType(typeof(TicketTypeResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketTypeResponseDTO>> Create([FromForm] CreateTicketTypeRequest request, CancellationToken ct = default)
    {
        var dto = new TicketTypeCreateDTO(
            request.Name,
            request.Price,
            request.EventId,
            request.Description,
            request.QuantityAvailable
        );

        var file = new FileUpload(
            request.BackgroundImage.OpenReadStream(),
            request.BackgroundImage.FileName,
            request.BackgroundImage.ContentType,
            request.BackgroundImage.Length
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
    public async Task<ActionResult<TicketTypeResponseDTO>> Update(Guid id, [FromBody] TicketTypeUpdateDTO dto, CancellationToken ct = default)
    {
        var command = _commandMapper.MapToCommand(id, dto);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Organizer, Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> Delete(Guid id, CancellationToken ct = default)
    {
        var command = new DeleteTicketTypeCommand(id);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }
}
