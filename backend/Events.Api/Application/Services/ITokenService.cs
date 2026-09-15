using Events.Api.Application.DTOs;

namespace Events.Api.Application.Services;

public interface ITokenService
{
    string GenerateJwtToken(UserResponseDTO user);
}