using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record EventCreateDTO
(
    [property: Required]
    string Title,

    [property: Required]
    string Description,

    [property: Required]
    string Country,

    [property: Required]
    string City,

    [property: Required]
    string Address,

    [property: Required]
    string MainImageURL,

    string? VenueName,

    DateTimeOffset StartOfEvent,

    DateTimeOffset EndOfEvent
);