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
public class EventSponsorshipController : ControllerBase
{
    private readonly ISender _sender;
    private readonly CommandMapper _commandMapper;

    public EventSponsorshipController(ISender sender, CommandMapper commandMapper)
    {
        _sender = sender;
        _commandMapper = commandMapper;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(EventSponsorshipResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var query = new GetEventSponsorshipByIdQuery(id);
        var sponsorship = await _sender.Send(query, ct);

        return Ok(sponsorship);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<EventSponsorshipResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetEventSponsorshipsQuery();
        var sponsorships = await _sender.Send(query, ct);

        return Ok(sponsorships);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<EventSponsorshipResponseDTO>> Create([FromBody] EventSponsorshipCreateDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> DeleteEventSponsorship(Guid id, CancellationToken ct = default)
    {
        var command = new DeleteEventSponsorshipCommand(id);
        await _sender.Send(command, ct);

        return Ok($"Sponzorstvo sa ID-jem {id} je uspesno izbrisano");
    }
}
