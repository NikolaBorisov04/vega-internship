using Microsoft.AspNetCore.Mvc;
using Events.Api.Services;
using Events.Api.DTOs;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound(new { message = $"Korisnik sa ID-jem {id} nije pronadjen." });
        }

        return Ok(user);
    }
    [HttpPost("register/customer")]
    public async Task<ActionResult<UserResponseDTO>> RegisterCustomer(
        [FromBody] RegisterCustomerDTO dto, 
        CancellationToken ct)
    {
        var result = await _userService.RegisterCustomerAsync(dto, ct);
        return CreatedAtAction(nameof(RegisterCustomer), new { id = result.Id }, result);
    }

    [HttpPost("register/organizer")]
    public async Task<ActionResult<UserResponseDTO>> RegisterOrganizer(
        [FromBody] RegisterOrganizerDTO dto, 
        CancellationToken ct)
    {
        var result = await _userService.RegisterOrganizerAsync(dto, ct);
        return CreatedAtAction(nameof(RegisterOrganizer), new { id = result.Id }, result);
    }

    [HttpPost("register/admin")]
    //[Authorize(Roles = "Admin")] ovo posle kad odradimo autorizaciju
    public async Task<ActionResult<UserResponseDTO>> RegisterAdmin(
        [FromBody] RegisterAdminDTO dto, 
        CancellationToken ct)
    {
        var result = await _userService.RegisterAdminAsync(dto, ct);
        return CreatedAtAction(nameof(RegisterAdmin), new { id = result.Id }, result);
    }
}