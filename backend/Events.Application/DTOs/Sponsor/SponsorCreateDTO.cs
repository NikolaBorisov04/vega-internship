using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record SponsorCreateDTO
(
    [param: Required]
    string Name,

    [param: Required]
    string ContactEmail,

    [param: Required]
    string Description,

    [param: Required]
    string LogoUrl,

    [param: Required]
    string TaxId,

    string? WebsiteUrl
);