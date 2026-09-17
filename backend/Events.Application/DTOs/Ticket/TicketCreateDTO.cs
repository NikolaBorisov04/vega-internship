using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record TicketCreateDTO
(
    [property: Required]
    string QRCodeURL,

    int? SeatNumber,

    bool IsUsed,

    DateTimeOffset? UsedAt,

    Guid TicketTypeId
);