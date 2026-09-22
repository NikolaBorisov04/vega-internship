namespace Events.Application.DTOs;

public record UpdateAdminDTO(
    string? Name = null,

    string? Email = null,

    string? Country = null,

    string? City = null,

    string? Address = null,

    string? PhoneNumber = null,

    string? CompanyName = null
) : UpdateOrganizerDTO(Name, Email, Country, City, Address, PhoneNumber) ;