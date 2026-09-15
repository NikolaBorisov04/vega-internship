namespace Events.Application.DTOs;

public sealed record LoginDTO
(
    string Email,
    string Password
);