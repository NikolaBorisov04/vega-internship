using Events.Domain.Entities;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class EventPhotoFactory
{
    public static EventPhoto Create(CreateEventPhotoCommand command, string imageUrl, string imagePublicId)
    {
        return new EventPhoto
        {
            Caption = command.Dto.Caption,
            EventId = command.Dto.EventId,
            Url = imageUrl,
            PublicId = imagePublicId
        };
    }
}