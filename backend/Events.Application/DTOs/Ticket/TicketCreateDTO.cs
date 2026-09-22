using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record TicketCreateDTO
(
    [param: Required]
    string QRCodeURL,

    int? SeatNumber,

    bool IsUsed,

    DateTimeOffset? UsedAt,

    Guid TicketTypeId
);