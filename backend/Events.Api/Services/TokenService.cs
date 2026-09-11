using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Events.Api.DTOs;

namespace Events.Api.Services;

public class TokenService : ITokenService
{
    public string GenerateJwtToken(UserResponseDTO user)
    {
        //.NET user secrets
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");

        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");   

        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT_KEY promenljiva nije pronađena u okruženju ili .env fajlu.");
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}