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
public class SponsorController : ControllerBase
{
    private readonly ISender _sender;
    private readonly CommandMapper _commandMapper;

    public SponsorController(ISender sender, CommandMapper commandMapper)
    {
        _sender = sender;
        _commandMapper = commandMapper;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SponsorResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var query = new GetSponsorByIdQuery(id);
        var sponsor = await _sender.Send(query, ct);

        return Ok(sponsor);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<SponsorResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct = default)
    {
        var query = new GetSponsorsQuery();
        var sponsors = await _sender.Send(query, ct);

        return Ok(sponsors);
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<SponsorResponseDTO>> Create([FromBody] SponsorCreateDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var result = await _sender.Send(command, ct);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> DeleteSponsor(Guid id, CancellationToken ct = default)
    {
        var command = new DeleteSponsorCommand(id);
        await _sender.Send(command, ct);

        return Ok($"Sponzor sa ID-jem {id} je uspesno izbrisan");
    }
}