using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public record RegisterAdminDTO
(
    [param: Required]
    string Name,

    [param: Required]
    string Email,

    [param: Required]
    string Password,

    [param: Required]
    string Country,

    [param: Required]
    string City,

    [param: Required]
    string Address,

    [param: Required]
    string PhoneNumber,

    [param: Required]
    string CompanyName
) : RegisterOrganizerDTO(Name, Email, Password, Country, City, Address, PhoneNumber, CompanyName);