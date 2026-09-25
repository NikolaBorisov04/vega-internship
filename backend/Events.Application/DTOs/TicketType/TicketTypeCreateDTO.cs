using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record TicketTypeCreateDTO
(
    [param: Required]
    string Name,

    decimal Price,

    Guid EventId,

    [param: Required]
    string Description,

    int QuantityAvailable
);