using Events.Application.DTOs;

namespace Events.Application.Services;

public interface ITokenService
{
    string GenerateJwtToken(UserResponseDTO user);
}