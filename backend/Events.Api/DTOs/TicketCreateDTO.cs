namespace Events.Api.DTOs;

public sealed record TicketCreateDTO
(
    string QRCodeURL,
    int? SeatNumber,
    bool IsUsed,
    DateTimeOffset? UsedAt,
    Guid TicketTypeId,
    Guid CustomerId
);