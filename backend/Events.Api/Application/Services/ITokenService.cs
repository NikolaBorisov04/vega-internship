using Events.Api.DTOs;
using Events.Api.Entities;

namespace Events.Api.Services;

public interface ITokenService
{
    string GenerateJwtToken(UserResponseDTO user);
}