namespace Events.Api.DTOs;

public sealed record TicketCreateDTO
(
    string QRCodeURL,
    int? SeatNumber,
    bool IsUsed,
    DateTime? UsedAt,
    Guid TicketTypeId,
    Guid CustomerId
);