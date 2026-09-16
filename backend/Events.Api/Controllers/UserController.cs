using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.Services;
using Events.Application.DTOs;
using MediatR;
using Events.Application.Queries;
using Events.Application.Mappers;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly CommandMapper _commandMapper;

    public UserController(ISender sender, CommandMapper commandMapper)
    {
        _sender = sender;
        _commandMapper = commandMapper;
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
    public async Task<IActionResult> GetAllAsync(CancellationToken ct)
    {
        var query = new GetUsersQuery();
        var users = await _sender.Send(query, ct);

        return Ok(users);
    }

    [HttpPost("register/customer")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDTO>> RegisterCustomer([FromBody] RegisterCustomerDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var user = await _sender.Send(command, ct);

        return Ok(user);
    }

    [HttpPost("register/organizer")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDTO>> RegisterOrganizer([FromBody] RegisterOrganizerDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var user = await _sender.Send(command, ct);

        return Ok(user);
    }

    [HttpPost("register/admin")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserResponseDTO>> RegisterAdmin([FromBody] RegisterAdminDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var user = await _sender.Send(command, ct);

        return Ok(user);
    }
}