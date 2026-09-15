using Events.Application.Commands;
using Events.Application.DTOs;
using Events.Application.Queries;

namespace Events.Application.Mappers;

public class CQMapper
{
    public CreateEventCommand MapToCommand(EventCreateDTO dto)
    {
        return new CreateEventCommand(
            dto.Title,
            dto.Description,
            dto.Country,
            dto.City,
            dto.Address,
            dto.MainImageURL,
            dto.VenueName,
            dto.StartOfEvent,
            dto.EndOfEvent
        );
    }
}