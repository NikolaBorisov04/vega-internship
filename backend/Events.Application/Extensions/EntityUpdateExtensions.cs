using Events.Application.DTOs;
using Events.Domain.Entities;

namespace Events.Application.Extensions;

public static class EntityUpdateExtensions
{
    public static void UpdateFrom(
        this TicketType ticketType,
        TicketTypeUpdateDTO dto)
    {
        bool modified = false;

        if (dto.Name is not null)
        {
            ticketType.Name = dto.Name;
            modified = true;
        }

        if (dto.Price.HasValue)
        {
            ticketType.Price = dto.Price.Value;
            modified = true;
        }

        if (dto.Description is not null)
        {
            ticketType.Description = dto.Description;
            modified = true;
        }

        if (dto.QuantityAvailable.HasValue)
        {
            ticketType.QuantityAvailable = dto.QuantityAvailable.Value;
            modified = true;
        }

        if (dto.TicketBackgroundImageUrl is not null)
        {
            ticketType.TicketBackgroundImageUrl = dto.TicketBackgroundImageUrl;
            modified = true;
        }

        if (modified)
            ticketType.ModifiedAt = DateTimeOffset.UtcNow;
    }

    public static void UpdateFrom(
        this Ticket ticket,
        TicketUpdateDTO dto)
    {
        bool modified = false;

        if (dto.QRCodeURL is not null)
        {
            ticket.QRCodeURL = dto.QRCodeURL;
            modified = true;
        }

        if (dto.SeatNumber.HasValue)
        {
            ticket.SeatNumber = dto.SeatNumber.Value;
            modified = true;
        }

        if (dto.IsUsed.HasValue)
        {
            ticket.IsUsed = dto.IsUsed.Value;
            modified = true;
        }

        if (dto.UsedAt.HasValue)
        {
            ticket.UsedAt = dto.UsedAt.Value;
            modified = true;
        }

        if (modified)
            ticket.ModifiedAt = DateTimeOffset.UtcNow;
    }

    public static void UpdateFrom(
        this Event _event,
        EventUpdateDTO dto)
    {
        bool modified = false;

        if (dto.Title is not null)
        {
            _event.Title = dto.Title;
            modified = true;
        }

        if (dto.Description is not null)
        {
            _event.Description = dto.Description;
            modified = true;
        }

        if (dto.Country is not null)
        {
            _event.Country = dto.Country;
            modified = true;
        }

        if (dto.City is not null)
        {
            _event.City = dto.City;
            modified = true;
        }

        if (dto.Address is not null)
        {
            _event.Address = dto.Address;
            modified = true;
        }

        if (dto.MainImageURL is not null)
        {
            _event.MainImageURL = dto.MainImageURL;
            modified = true;
        }

        if (dto.VenueName is not null)
        {
            _event.VenueName = dto.VenueName;
            modified = true;
        }

        if (dto.StartOfEvent.HasValue)
        {
            _event.StartOfEvent = dto.StartOfEvent.Value;
            modified = true;
        }

        if (dto.EndOfEvent.HasValue)
        {
            _event.EndOfEvent = dto.EndOfEvent.Value;
            modified = true;
        }

        if (modified)
            _event.ModifiedAt = DateTimeOffset.UtcNow;
    }

    public static void UpdateFrom(
        this EventPhoto eventPhoto,
        EventPhotoUpdateDTO dto)
    {
        bool modified = false;

        if (dto.Url is not null)
        {
            eventPhoto.Url = dto.Url;
            modified = true;
        }

        if (dto.Caption is not null)
        {
            eventPhoto.Caption = dto.Caption;
            modified = true;
        }

        if (modified)
            eventPhoto.ModifiedAt = DateTimeOffset.UtcNow;
    }

    public static void UpdateFrom(
        this EventSponsorship eventSponsorship,
        EventSponsorshipUpdateDTO dto)
    {
        if (dto.ContributionAmount.HasValue)
        {
            eventSponsorship.ContributionAmount = dto.ContributionAmount.Value;
            eventSponsorship.ModifiedAt = DateTimeOffset.UtcNow;
        }
    }

    public static void UpdateFrom(
        this Sponsor sponsor,
        SponsorUpdateDTO dto)
    {
        bool modified = false;

        if (dto.Name is not null)
        {
            sponsor.Name = dto.Name;
            modified = true;
        }

        if (dto.ContactEmail is not null)
        {
            sponsor.ContactEmail = dto.ContactEmail;
            modified = true;
        }

        if (dto.Description is not null)
        {
            sponsor.Description = dto.Description;
            modified = true;
        }

        if (dto.LogoUrl is not null)
        {
            sponsor.LogoUrl = dto.LogoUrl;
            modified = true;
        }

        if (dto.TaxId is not null)
        {
            sponsor.TaxId = dto.TaxId;
            modified = true;
        }

        if (dto.WebsiteUrl is not null)
        {
            sponsor.WebsiteUrl = dto.WebsiteUrl;
            modified = true;
        }

        if (modified)
            sponsor.ModifiedAt = DateTimeOffset.UtcNow;
    }

    public static void UpdateFrom(
        this Customer customer,
        UpdateCustomerDTO dto)
    {
        bool modified = false;

        if (dto.Name is not null)
        {
            customer.Name = dto.Name;
            modified = true;
        }

        if (dto.Email is not null)
        {
            customer.Email = dto.Email;
            modified = true;
        }

        if (dto.Country is not null)
        {
            customer.Country = dto.Country;
            modified = true;
        }

        if (dto.City is not null)
        {
            customer.City = dto.City;
            modified = true;
        }

        if (dto.Address is not null)
        {
            customer.Address = dto.Address;
            modified = true;
        }

        if (dto.PhoneNumber is not null)
        {
            customer.PhoneNumber = dto.PhoneNumber;
            modified = true;
        }

        if (modified)
            customer.ModifiedAt = DateTimeOffset.UtcNow;
    }
}