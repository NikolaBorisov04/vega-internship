namespace Events.Api.DTOs;

public record UserResponseDTO(
    Guid Id,
    string Name,
    string Email,
    UserRole Role,
    string? CompanyName = null,
    bool? Validated = null
);