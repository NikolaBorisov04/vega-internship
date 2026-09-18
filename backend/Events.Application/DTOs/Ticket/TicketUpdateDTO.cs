namespace Events.Application.DTOs;

public sealed record TicketUpdateDTO(
    string? QRCodeURL,
    int? SeatNumber,
    bool? IsUsed,
    DateTimeOffset? UsedAt
);