using Events.Domain.Enums;

namespace Events.Application.DTOs;

public sealed record EventSponsorshipResponseDTO(
    Guid Id,
    decimal ContributionAmount,
    PaymentStatus PaymentStatus,
    Guid EventId,
    Guid SponsorId,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt
);