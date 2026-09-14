using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Api.Services;
using Events.Api.DTOs;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SponsorController : ControllerBase
{
    private readonly ISponsorService _sponsorService;

    public SponsorController(ISponsorService sponsorService)
    {
        _sponsorService = sponsorService;
    }

    [HttpGet("{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SponsorResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var sponsor = await _sponsorService.GetByIdAsync(id);

        if (sponsor == null)
        {
            return NotFound(new { message = $"Sponzor sa ID-jem {id} nije pronadjena." });
        }

        return Ok(sponsor);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<SponsorResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync()
    {
        var sponsors = await _sponsorService.GetAllAsync();
        if (!sponsors.Any())
            return NotFound(new { message = "Nema sponzora u bazi." });

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
        var result = await _sponsorService.CreateAsync(dto, ct);
            
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }
}