using System.ComponentModel.DataAnnotations;
using Events.Domain.Enums;

namespace Events.Application.DTOs;

public sealed record UserResponseDTO
(
    Guid Id,

    [property: Required]
    string Name,

    [property: Required]
    string Email,

    UserRole Role,

    DateTimeOffset CreatedAt,

    DateTimeOffset ModifiedAt,

    string? Country = null,

    string? City = null,

    string? Address = null,

    string? PhoneNumber = null,

    string? CompanyName = null,

    bool? Validated = null
);