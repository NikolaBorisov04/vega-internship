using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record TicketTypeCreateDTO
(
    [property: Required]
    string Name,

    decimal Price,

    Guid EventId,

    [property: Required]
    string Description,

    int QuantityAvailable,

    [property: Required]
    string TicketBackgroundImageUrl
);