namespace Events.Application.DTOs;

public sealed record SponsorUpdateDTO
(
    string? Name,

    string? ContactEmail,

    string? Description,

    string? LogoUrl,

    string? TaxId,

    string? WebsiteUrl
);