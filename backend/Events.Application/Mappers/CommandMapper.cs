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
}