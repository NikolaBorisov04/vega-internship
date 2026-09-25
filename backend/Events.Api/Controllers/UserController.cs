using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Events.Application.DTOs;
using MediatR;
using Events.Application.Queries;
using Events.Application.Mappers;

using Events.Application.Commands;

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _sender.Send(query, ct);

        return Ok(user);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<UserResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct)
    {
        var query = new GetUsersQuery();
        var users = await _sender.Send(query, ct);

        return Ok(users);
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserResponseDTO>> GetCurrentUserAsync(CancellationToken ct)
    {
        var query = new GetCurrentUserQuery();

        var user = await _sender.Send(query, ct);

        return Ok(user);
    }

    [HttpGet("organizer/event/{eventId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDTO>> GetOrganizerByEventId(Guid eventId, CancellationToken ct)
    {
        var query = new GetOrganizerByEventIdQuery(eventId);
        var organizer = await _sender.Send(query, ct);

        return Ok(organizer);
    }

    [HttpPost("register/customer")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDTO>> RegisterCustomer([FromBody] RegisterCustomerDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var user = await _sender.Send(command, ct);

        return StatusCode(StatusCodes.Status201Created, user);
    }

    [HttpPost("register/organizer")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponseDTO>> RegisterOrganizer([FromBody] RegisterOrganizerDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var user = await _sender.Send(command, ct);

        return StatusCode(StatusCodes.Status201Created, user);
    }

    [HttpPost("register/admin")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserResponseDTO>> RegisterAdmin([FromBody] RegisterAdminDTO dto, CancellationToken ct)
    {
        var command = _commandMapper.MapToCommand(dto);
        var user = await _sender.Send(command, ct);

        return StatusCode(StatusCodes.Status201Created, user);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> DeleteUser(Guid id, CancellationToken ct = default)
    {
        var command = new DeleteUserCommand(id);
        await _sender.Send(command, ct);

        return Ok($"Nalog sa ID-jem {id} je uspesno izbrisan");
    }
}