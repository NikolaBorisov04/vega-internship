namespace Events.Api.DTOs;

public record TicketResponseDTO(
    Guid Id,
    string TicketCode,
    string QRCodeURL,
    int? SeatNumber,
    bool IsUsed,
    DateTime? UsedAt,
    Guid TicketTypeId,
    Guid CustomerId
);