namespace Events.Application.DTOs;

public record UpdateCustomerDTO(
    string? Name = null,

    string? Email = null,

    string? Country = null,

    string? City = null,

    string? Address = null,
    
    string? PhoneNumber = null
);