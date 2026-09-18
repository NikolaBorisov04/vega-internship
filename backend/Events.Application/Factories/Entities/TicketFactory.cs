using Events.Domain.Entities;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class TicketFactory
{
    public static Ticket Create(CreateTicketCommand command, Guid customerId)
    {
        return new Ticket
        {
            QRCodeURL = command.dto.QRCodeURL,
            SeatNumber = command.dto.SeatNumber,
            IsUsed = command.dto.IsUsed,
            UsedAt = command.dto.UsedAt,
            TicketTypeId = command.dto.TicketTypeId,
            CustomerId = customerId
        };
    }
}