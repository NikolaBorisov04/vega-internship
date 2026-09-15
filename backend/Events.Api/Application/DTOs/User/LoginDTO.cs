namespace Events.Api.Application.DTOs;

public sealed record LoginDTO
(
    string Email,
    string Password
);