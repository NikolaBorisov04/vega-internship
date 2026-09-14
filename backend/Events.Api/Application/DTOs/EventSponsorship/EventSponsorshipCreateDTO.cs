using Events.Api.Entities;

namespace Events.Api.DTOs;

public sealed record EventSponsorshipCreateDTO(
    decimal ContributionAmount,
    Guid EventId,
    Guid SponsorId
);