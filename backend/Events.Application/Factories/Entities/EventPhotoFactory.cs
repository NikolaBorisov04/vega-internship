using Events.Domain.Entities;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class EventPhotoFactory
{
    public static EventPhoto Create(CreateEventPhotoCommand command)
    {
        return new EventPhoto
        {
            Caption = command.dto.Caption,
            EventId = command.dto.EventId
        };
    }
}