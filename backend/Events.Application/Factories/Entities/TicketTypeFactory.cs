using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class TicketTypeFactory
{
    public static TicketType Create(CreateTicketTypeCommand command)
    {
        return new TicketType
        {
            Name = command.dto.Name,
            Price = command.dto.Price,
            EventId = command.dto.EventId,
            Description = command.dto.Description,
            QuantityAvailable = command.dto.QuantityAvailable,
            TicketBackgroundImageUrl = command.dto.TicketBackgroundImageUrl
        };
    }
}