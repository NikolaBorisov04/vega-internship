using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class TicketTypeFactory
{
    public static TicketType Create(CreateTicketTypeCommand command, string imageUrl, string imagePublicId)
    {
        return new TicketType
        {
            Name = command.Dto.Name,
            Price = command.Dto.Price,
            EventId = command.Dto.EventId,
            Description = command.Dto.Description,
            QuantityAvailable = command.Dto.QuantityAvailable,
            ImageUrl = imageUrl,
            ImagePublicId = imagePublicId
        };
    }
}