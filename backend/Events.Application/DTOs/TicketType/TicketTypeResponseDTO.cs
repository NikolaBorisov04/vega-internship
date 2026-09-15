namespace Events.Application.DTOs;

public sealed record TicketTypeResponseDTO
(
    Guid Id,
    string Name,
    decimal Price,
    Guid EventId,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt,
    string Description,
    int QuantityAvailable,
    string TicketBackgroundImageUrl
);