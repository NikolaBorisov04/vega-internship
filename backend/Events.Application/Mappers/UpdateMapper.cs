using Events.Application.DTOs;
using Events.Domain.Entities;

namespace Events.Application.Mappers;

public static class UpdateMapper
{
    public static void UpdateEntity(
        TicketTypeUpdateDTO dto,
        TicketType ticketType)
    {
        if (dto.Name is not null)
            ticketType.Name = dto.Name;

        if (dto.Price.HasValue)
            ticketType.Price = dto.Price.Value;

        if (dto.Description is not null)
            ticketType.Description = dto.Description;

        if (dto.QuantityAvailable.HasValue)
            ticketType.QuantityAvailable = dto.QuantityAvailable.Value;

        if (dto.TicketBackgroundImageUrl is not null)
            ticketType.TicketBackgroundImageUrl = dto.TicketBackgroundImageUrl;
    }

    public static void UpdateEntity(
        TicketUpdateDTO dto,
        Ticket ticketType)
    {
        if (dto.QRCodeURL is not null)
            ticketType.QRCodeURL = dto.QRCodeURL;

        if (dto.SeatNumber.HasValue)
            ticketType.SeatNumber = dto.SeatNumber.Value;

        if(dto.IsUsed.HasValue)
            ticketType.IsUsed = dto.IsUsed.Value;
        
        if(dto.UsedAt.HasValue)
            ticketType.UsedAt = dto.UsedAt.Value;
    }
}