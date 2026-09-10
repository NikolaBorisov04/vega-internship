using Events.Api.DTOs;
using Events.Api.Entities;

namespace Events.Api.Extensions;
public static class TicketMappingExtensions
{
    public static IQueryable<TicketResponseDTO> ToTicketResponseDTO(this IQueryable<Ticket> query)
    {
        return query.Select(t => new TicketResponseDTO(
            t.Id,
            t.TicketCode,
            t.QRCodeURL!,
            t.SeatNumber,
            t.IsUsed,
            t.UsedAt,
            t.TicketTypeId,
            t.CustomerId
        ));
    }
}