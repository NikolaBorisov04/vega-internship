namespace Events.Application.DTOs;

public record RegisterOrganizerDTO
(
    string Name,
    string Email,
    string Password,
    string Country,
    string City,
    string Address,
    string PhoneNumber,
    string CompanyName
) : RegisterCustomerDTO(Name, Email, Password, Country, City, Address, PhoneNumber);