using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record TicketResponseDTO
(
    Guid Id,

    [property: Required]
    string TicketCode,

    [property: Required]
    string QRCodeURL,

    int? SeatNumber,

    bool IsUsed,

    DateTimeOffset? UsedAt,

    Guid TicketTypeId,

    Guid CustomerId
);