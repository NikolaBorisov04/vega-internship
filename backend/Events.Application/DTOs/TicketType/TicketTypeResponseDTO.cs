using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record TicketTypeResponseDTO
(
    Guid Id,

    [property: Required]
    string Name,

    decimal Price,

    Guid EventId,

    DateTimeOffset CreatedAt,

    DateTimeOffset ModifiedAt,

    [property: Required]
    string Description,

    int QuantityAvailable,

    [property: Required]
    string ImageUrl,

    [property: Required]
    string ImagePublicId
);