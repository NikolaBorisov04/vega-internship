namespace Events.Application.DTOs;

public record RegisterCustomerDTO
(
    string Name,
    string Email,
    string Password,
    string Country,
    string City,
    string Address,
    string PhoneNumber
);