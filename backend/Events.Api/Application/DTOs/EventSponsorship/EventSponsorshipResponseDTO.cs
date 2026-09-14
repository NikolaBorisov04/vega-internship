using Events.Api.Entities;

namespace Events.Api.DTOs;

public sealed record EventSponsorshipResponseDTO(
    Guid Id,
    decimal ContributionAmount,
    PaymentStatus PaymentStatus,
    Guid EventId,
    Guid SponsorId,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);