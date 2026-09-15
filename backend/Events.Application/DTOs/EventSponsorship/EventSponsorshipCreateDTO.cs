namespace Events.Application.DTOs;

public sealed record EventSponsorshipCreateDTO(
    decimal ContributionAmount,
    Guid EventId,
    Guid SponsorId
);