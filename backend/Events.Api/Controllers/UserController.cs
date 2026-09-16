using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.Services;
using Events.Application.DTOs;
using MediatR;
using Events.Application.Queries;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ISender sender)
    {
        _userService = userService;
        _sender = sender;
    }

    [HttpGet("{id:Guid}")]
    [Authorize]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _sender.Send(query, ct);

        return Ok(user);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<UserResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();
        if (!users.Any())
            return NotFound(new { message = "Nema korisnika u bazi." });

        return Ok(users);
    }

    [HttpPost("register/customer")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDTO>> RegisterCustomer([FromBody] RegisterCustomerDTO dto, CancellationToken ct)
    {
        var result = await _userService.RegisterCustomerAsync(dto, ct);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPost("register/organizer")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDTO>> RegisterOrganizer([FromBody] RegisterOrganizerDTO dto, CancellationToken ct)
    {
        var result = await _userService.RegisterOrganizerAsync(dto, ct);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPost("register/admin")]
    //[Authorize(Roles = "Admin")] ovo sam ostavio ovako jer je lakse za testiranje
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserResponseDTO>> RegisterAdmin([FromBody] RegisterAdminDTO dto, CancellationToken ct)
    {
        var result = await _userService.RegisterAdminAsync(dto, ct);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
    }
}