using Events.Api.Extensions;
using Events.Application.Commands;
using Events.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserResponseDTO>> Login([FromBody] LoginDTO dto, CancellationToken ct)
    {
        var command = new LoginCommand(dto);

        var result = await _sender.Send(command, ct);

        Response.SetAccessTokenCookie(result.Token);

        return Ok(result.User);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Logout()
    {
        Response.DeleteAccessTokenCookie();

        return NoContent();
    }
}