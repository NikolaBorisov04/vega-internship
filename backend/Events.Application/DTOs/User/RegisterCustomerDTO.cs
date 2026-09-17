using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public record RegisterCustomerDTO
(
    [property: Required]
    string Name,

    [property: Required]
    string Email,

    [property: Required]
    string Password,

    [property: Required]
    string Country,

    [property: Required]
    string City,

    [property: Required]
    string Address,

    [property: Required]
    string PhoneNumber
);