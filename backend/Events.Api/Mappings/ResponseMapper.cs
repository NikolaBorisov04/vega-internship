using Events.Api.DTOs;
using Events.Api.Entities;

namespace Events.Api.Mappings;

public class ResponseMapper
{
    public UserResponseDTO MapToResponse(User user)
    {
        return user switch
        {
            Admin admin => new UserResponseDTO(admin.Id, admin.Name, admin.Email, admin.Role, admin.CreatedAt, admin.ModifiedAt, admin.Country, admin.City, admin.Address, admin.PhoneNumber, admin.CompanyName, admin.Validated),
            Organizer organizer => new UserResponseDTO(organizer.Id, organizer.Name, organizer.Email, organizer.Role, organizer.CreatedAt, organizer.ModifiedAt, organizer.Country, organizer.City, organizer.Address, organizer.PhoneNumber, organizer.CompanyName, organizer.Validated),
            Customer customer => new UserResponseDTO(customer.Id, customer.Name, customer.Email, customer.Role, customer.CreatedAt, customer.ModifiedAt, customer.Country, customer.City, customer.Address, customer.PhoneNumber),
            _ => new UserResponseDTO(user.Id, user.Name, user.Email, user.Role, user.CreatedAt, user.ModifiedAt)
        };
    }

    public TicketResponseDTO MapToResponse(Ticket ticket)
    {
        return new TicketResponseDTO(
            ticket.Id,
            ticket.TicketCode,
            ticket.QRCodeURL,
            ticket.SeatNumber,
            ticket.IsUsed,
            ticket.UsedAt,
            ticket.TicketTypeId,
            ticket.CustomerId
        );
    }

    public EventResponseDTO MapToResponse(Event _event)
    {
        return new EventResponseDTO(
            _event.Id,
            _event.Title,
            _event.Description,
            _event.Country,
            _event.City,
            _event.Address,
            _event.MainImageURL,
            _event.VenueName,
            _event.DateAndTimeOfEvent,
            _event.OrganizerId,
            _event.EventPhotosURL
        );
    }

    public SponsorResponseDTO MapToResponse(Sponsor sponsor)
    {
        return new SponsorResponseDTO(
            sponsor.Id,
            sponsor.Name,
            sponsor.ContactEmail,
            sponsor.Description,
            sponsor.LogoUrl,
            sponsor.WebsiteUrl
        );
    }

    public TicketTypeResponseDTO MapToResponse(TicketType ticketType)
    {
        return new TicketTypeResponseDTO(
            ticketType.Id,
            ticketType.Name,
            ticketType.Price,
            ticketType.EventId,
            ticketType.CreatedAt,
            ticketType.ModifiedAt,
            ticketType.Description,
            ticketType.QuantityAvailable,
            ticketType.TicketBackgroundImageUrl
        );
    }
}