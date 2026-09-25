namespace Events.Application.DTOs;

public sealed record LoginResultDTO(
    UserResponseDTO User,
    string Token
);