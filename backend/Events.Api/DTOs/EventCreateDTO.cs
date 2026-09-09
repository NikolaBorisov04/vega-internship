namespace Events.Api.DTOs;

public record EventCreateDTO(
    string Title,
    string Description,
    string Country,
    string City,
    string Address,
    string MainImageURL,
    string? VenueName,
    DateTimeOffset DateAndTimeOfEvent,
    Guid OrganizerId
);