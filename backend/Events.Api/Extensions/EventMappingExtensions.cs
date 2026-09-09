using Events.Api.DTOs;
using Events.Api.Entities;

namespace Events.Api.Extensions;
public static class EventMappingExtensions
{
    public static IQueryable<EventResponseDTO> ToEventResponseDTO(this IQueryable<Event> query)
    {
        return query.Select(e => new EventResponseDTO(
            e.Id,
            e.Title,
            e.Description,
            e.Country,
            e.City,
            e.Address,
            e.MainImageURL,
            e.VenueName,
            e.DateAndTimeOfEvent,
            e.OrganizerId,
            e.EventPhotosURL
        ));
    }
}