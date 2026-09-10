namespace Events.Api.DTOs;

public sealed record LoginDTO(
    string Email,
    string Password
);