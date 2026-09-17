using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record SponsorCreateDTO
(
    [property: Required]
    string Name,

    [property: Required]
    string ContactEmail,

    [property: Required]
    string Description,

    [property: Required]
    string LogoUrl,

    [property: Required]
    string TaxId,

    string? WebsiteUrl
);