using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record SponsorResponseDTO
(
    Guid Id,

    [property: Required]
    string Name,

    [property: Required]
    string ContactEmail,

    [property: Required]
    string Description,

    [property: Required]
    string LogoUrl,

    string? WebsiteUrl
);