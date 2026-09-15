namespace Events.Api.Application.DTOs;

public sealed record SponsorResponseDTO
(
    Guid Id,
    string Name,
    string ContactEmail,
    string Description,
    string LogoUrl,
    string? WebsiteUrl
);