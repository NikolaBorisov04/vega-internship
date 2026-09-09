namespace Events.Api.DTOs;

public record TicketCreateDTO(
    string QRCodeURL,
    int? SeatNumber,
    bool IsUsed,
    DateTime? UsedAt,
    Guid TicketTypeId,
    Guid CustomerId
);