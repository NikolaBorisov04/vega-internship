namespace Events.Api.DTOs;

public sealed record UserResponseDTO(
    Guid Id,
    string Name,
    string Email,
    UserRole Role,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt,
    string? County = null,
    string? City = null,
    string? Address = null,
    string? PhoneNumber = null,
    string? CompanyName = null,
    bool? Validated = null
);