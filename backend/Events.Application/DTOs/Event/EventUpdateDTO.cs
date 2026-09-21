namespace Events.Application.DTOs;

public sealed record EventUpdateDTO
(
    string? Title,

    string? Description,

    string? Country,

    string? City,

    string? Address,

    string? VenueName,

    DateTimeOffset? StartOfEvent,

    DateTimeOffset? EndOfEvent
);