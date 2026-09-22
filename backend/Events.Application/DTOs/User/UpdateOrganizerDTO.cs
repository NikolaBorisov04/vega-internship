namespace Events.Application.DTOs;

public record UpdateOrganizerDTO(
    string? Name = null,

    string? Email = null,

    string? Country = null,

    string? City = null,

    string? Address = null,

    string? PhoneNumber = null,

    string? CompanyName = null
) : UpdateCustomerDTO(Name, Email, Country, City, Address, PhoneNumber) ;