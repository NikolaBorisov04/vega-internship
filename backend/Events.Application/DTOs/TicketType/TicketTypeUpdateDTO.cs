namespace Events.Application.DTOs;

public sealed record TicketTypeUpdateDTO
(
    string? Name,
    decimal? Price,
    string? Description,
    int? QuantityAvailable,
    string? TicketBackgroundImageUrl
);
