using Events.Application.Commands;
using Events.Application.DTOs;
using Events.Domain.Enums;

namespace Events.Application.Mappers;

public class CommandMapper
{
    public CreateEventCommand MapToCommand(EventCreateDTO dto)
    {
        return new CreateEventCommand(dto);
    }
    public CreateUserCommand MapToCommand(RegisterCustomerDTO dto)
    {
        return new CreateUserCommand(
            dto.Name,
            dto.Email,
            UserRole.Customer,
            dto.Password,
            dto.Country,
            dto.City,
            dto.Address,
            dto.PhoneNumber
        );
    }

    public CreateUserCommand MapToCommand(RegisterOrganizerDTO dto)
    {
        return new CreateUserCommand(
            dto.Name,
            dto.Email,
            UserRole.Organizer,
            dto.Password,
            dto.Country,
            dto.City,
            dto.Address,
            dto.PhoneNumber,
            dto.CompanyName
        );
    }

    public CreateUserCommand MapToCommand(RegisterAdminDTO dto)
    {
        return new CreateUserCommand(
            dto.Name,
            dto.Email,
            UserRole.Admin,
            dto.Password,
            dto.Country,
            dto.City,
            dto.Address,
            dto.PhoneNumber,
            dto.CompanyName
        );
    }

    public CreateSponsorCommand MapToCommand(SponsorCreateDTO dto)
    {
        return new CreateSponsorCommand(dto);
    }

    public UpdateSponsorCommand MapToCommand(Guid id, SponsorUpdateDTO dto)
    {
        return new UpdateSponsorCommand(id, dto);
    }

    public CreateEventPhotoCommand MapToCommand(EventPhotoCreateDTO dto)
    {
        return new CreateEventPhotoCommand(dto);
    }

    public UpdateEventPhotoCommand MapToCommand(Guid id, EventPhotoUpdateDTO dto)
    {
        return new UpdateEventPhotoCommand(id, dto);
    }

    public CreateEventSponsorshipCommand MapToCommand(EventSponsorshipCreateDTO dto)
    {
        return new CreateEventSponsorshipCommand(dto);
    }

    public UpdateEventSponsorshipCommand MapToCommand(Guid id, EventSponsorshipUpdateDTO dto)
    {
        return new UpdateEventSponsorshipCommand(id, dto);
    }

    public CreateTicketTypeCommand MapToCommand(TicketTypeCreateDTO dto)
    {
        return new CreateTicketTypeCommand(dto);
    }

    public UpdateTicketTypeCommand MapToCommand(Guid id, TicketTypeUpdateDTO dto)
    {
        return new UpdateTicketTypeCommand(id, dto);
    }

    public CreateTicketCommand MapToCommand(TicketCreateDTO dto)
    {
        return new CreateTicketCommand(dto);
    }

    public UpdateTicketCommand MapToCommand(Guid id, TicketUpdateDTO dto)
    {
        return new UpdateTicketCommand(id, dto);
    }

    public UpdateEventCommand MapToCommand(Guid id, EventUpdateDTO dto)
    {
        return new UpdateEventCommand(id, dto);
    }
}
