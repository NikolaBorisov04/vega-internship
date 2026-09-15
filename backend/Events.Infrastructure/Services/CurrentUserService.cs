using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Events.Application.Services;

namespace Events.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || user.Identity?.IsAuthenticated != true)
            {
                throw new UnauthorizedAccessException("Korisnik nije autentifikovan.");
            }

            var userIdClaim = user.FindFirst(JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException(
                    "JWT token ne sadrži ID korisnika.");
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException(
                    "ID korisnika u JWT tokenu nije validan.");
            }

            return userId;
        }
    }
    public bool IsAdmin
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;

            return user?.IsInRole("Admin") == true;
        }
    }
}