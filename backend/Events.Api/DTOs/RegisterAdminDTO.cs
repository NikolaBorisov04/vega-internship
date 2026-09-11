namespace Events.Api.DTOs;

public record RegisterAdminDTO
(
    string Name,
    string Email,
    string Password,
    string Country,
    string City,
    string Address,
    string PhoneNumber,
    string CompanyName
) : RegisterOrganizerDTO(Name, Email, Password, Country, City, Address, PhoneNumber, CompanyName);