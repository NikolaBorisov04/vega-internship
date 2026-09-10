namespace Events.Api.DTOs;

public sealed record SponsorResponseDTO(
    Guid Id,
    string Name,
    string ContactEmail,
    string Description,
    string LogoUrl,
    string? WebsiteUrl
);