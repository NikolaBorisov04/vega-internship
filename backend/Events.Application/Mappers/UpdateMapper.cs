using Events.Application.DTOs;
using Events.Domain.Entities;

namespace Events.Application.Mappers;

public static class UpdateMapper
{
    public static void UpdateEntity(TicketTypeUpdateDTO dto, TicketType ticketType)
    {
        if (dto.Name is not null)
        {
            ticketType.Name = dto.Name;
            ticketType.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if (dto.Price.HasValue)
        {
            ticketType.Price = dto.Price.Value;
            ticketType.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if (dto.Description is not null)
        {
            ticketType.Description = dto.Description;
            ticketType.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if (dto.QuantityAvailable.HasValue)
        {
            ticketType.QuantityAvailable = dto.QuantityAvailable.Value;
            ticketType.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if (dto.TicketBackgroundImageUrl is not null)
        {
            ticketType.TicketBackgroundImageUrl = dto.TicketBackgroundImageUrl;
            ticketType.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }

    public static void UpdateEntity(TicketUpdateDTO dto, Ticket ticket)
    {
        if (dto.QRCodeURL is not null)
        {
            ticket.QRCodeURL = dto.QRCodeURL;
            ticket.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if (dto.SeatNumber.HasValue)
        {
            ticket.SeatNumber = dto.SeatNumber.Value;
            ticket.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if(dto.IsUsed.HasValue)
        {
            ticket.IsUsed = dto.IsUsed.Value;
            ticket.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if(dto.UsedAt.HasValue)
        {   
            ticket.UsedAt = dto.UsedAt.Value;
            ticket.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }

    public static void UpdateEntity(EventUpdateDTO dto, Event _event)
    {
        if (dto.Title is not null)
        {
            _event.Title = dto.Title;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if (dto.Description is not null)
        {
            _event.Description = dto.Description;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if(dto.Country is not null)
        {
            _event.Country = dto.Country;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }
        
        if(dto.City is not null)
        {
            _event.City = dto.City;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if(dto.Address is not null)
        {
            _event.Address = dto.Address;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if(dto.MainImageURL is not null)
        {
            _event.MainImageURL = dto.MainImageURL;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }
        
        if(dto.VenueName is not null)
        {
            _event.VenueName = dto.VenueName;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }
        
        if(dto.StartOfEvent.HasValue)
        {
            _event.StartOfEvent = dto.StartOfEvent.Value;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if(dto.EndOfEvent.HasValue)
        {
            _event.EndOfEvent = dto.EndOfEvent.Value;
            _event.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }

    public static void UpdateEntity(EventPhotoUpdateDTO dto, EventPhoto eventPhoto)
    {
        if (dto.Url is not null)
        {
            eventPhoto.Url = dto.Url;
            eventPhoto.ModifiedAt = DateTimeOffset.UtcNow;
        }

        if (dto.Caption is not null)
        {
            eventPhoto.Caption = dto.Caption;
            eventPhoto.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }

    public static void UpdateEntity(EventSponsorshipUpdateDTO dto, EventSponsorship eventSponsorship)
    {
        if(dto.ContributionAmount.HasValue)
        {
            eventSponsorship.ContributionAmount = dto.ContributionAmount.Value;
            eventSponsorship.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }

    public static void UpdateEntity(SponsorUpdateDTO dto, Sponsor sponsor)
    {
        if(dto.Name is not null)
        {
            sponsor.Name = dto.Name;
            sponsor.CreatedAt = DateTimeOffset.UtcNow;
        }

        if(dto.ContactEmail is not null)
        {
            sponsor.ContactEmail = dto.ContactEmail;
            sponsor.CreatedAt = DateTimeOffset.UtcNow;
        }

        if(dto.Description is not null)
        {
            sponsor.Description = dto.Description;
            sponsor.CreatedAt = DateTimeOffset.UtcNow;
        }

        if(dto.LogoUrl is not null)
        {
            sponsor.LogoUrl = dto.LogoUrl;
            sponsor.CreatedAt = DateTimeOffset.UtcNow;
        }

        if(dto.TaxId is not null)
        {
            sponsor.TaxId = dto.TaxId;
            sponsor.CreatedAt = DateTimeOffset.UtcNow;
        }

        if(dto.WebsiteUrl is not null)
        {
            sponsor.WebsiteUrl = dto.WebsiteUrl;
            sponsor.CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}