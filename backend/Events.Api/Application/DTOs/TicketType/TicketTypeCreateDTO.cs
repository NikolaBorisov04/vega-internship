namespace Events.Api.Application.DTOs;

public sealed record TicketTypeCreateDTO
(
    string Name,
    decimal Price,
    Guid EventId,
    string Description,
    int QuantityAvailable,
    string TicketBackgroundImageUrl
);