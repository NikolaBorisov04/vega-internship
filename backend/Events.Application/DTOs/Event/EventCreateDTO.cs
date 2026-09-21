using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record EventCreateDTO
(
    [param: Required]
    string Title,

    [param: Required]
    string Description,

    [param: Required]
    string Country,

    [param: Required]
    string City,

    [param: Required]
    string Address,

    string? VenueName,

    DateTimeOffset StartOfEvent,

    DateTimeOffset EndOfEvent
);