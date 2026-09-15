namespace Events.Api.Application.DTOs;

public sealed record TicketResponseDTO
(
    Guid Id,
    string TicketCode,
    string QRCodeURL,
    int? SeatNumber,
    bool IsUsed,
    DateTimeOffset? UsedAt,
    Guid TicketTypeId,
    Guid CustomerId
);