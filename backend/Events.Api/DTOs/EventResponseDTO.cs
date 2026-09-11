namespace Events.Api.DTOs;

public sealed record EventResponseDTO
(
    Guid Id,
    string Title,
    string Description,
    string Country,
    string City,
    string Address,
    string MainImageURL,
    string? VenueName,
    DateTimeOffset DateAndTimeOfEvent,
    Guid OrganizerId,
    IEnumerable<string> EventPhotosURL
);