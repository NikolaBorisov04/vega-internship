namespace Events.Application.DTOs;

public sealed record TicketCreateDTO
(
    string QRCodeURL,
    int? SeatNumber,
    bool IsUsed,
    DateTimeOffset? UsedAt,
    Guid TicketTypeId
);