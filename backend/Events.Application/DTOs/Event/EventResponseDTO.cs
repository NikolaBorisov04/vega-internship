using System.ComponentModel.DataAnnotations;
using Events.Domain.Enums;

namespace Events.Application.DTOs;

public sealed record EventResponseDTO
(
    Guid Id,

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

    [property: Required]
    string MainImagePublicId,

    string? VenueName,

    EventPriority Priority,

    DateTimeOffset StartOfEvent,

    DateTimeOffset EndOfEvent,

    Guid OrganizerId
);