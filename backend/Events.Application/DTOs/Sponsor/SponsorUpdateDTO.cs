namespace Events.Application.DTOs;

public sealed record SponsorUpdateDTO
(
    string? Name,

    string? ContactEmail,

    string? Description,

    string? ImageUrl,

    string? ImagePublicId,

    string? TaxId,

    string? WebsiteUrl
);