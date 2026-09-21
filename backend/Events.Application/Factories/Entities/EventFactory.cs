using Events.Domain.Entities;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class EventFactory
{
    public static Event Create(CreateEventCommand command, Guid organizerId, string imageUrl, string imagePublicId)
    {
        return new Event
        {
            Title = command.dto.Title,
            Description = command.dto.Description,
            Country = command.dto.Country,
            City = command.dto.City,
            Address = command.dto.Address,
            VenueName = command.dto.VenueName,
            StartOfEvent = command.dto.StartOfEvent,
            EndOfEvent = command.dto.EndOfEvent,
            OrganizerId = organizerId,
            MainImageURL = imageUrl,
            MainImagePublicId = imagePublicId
        };
    }
}