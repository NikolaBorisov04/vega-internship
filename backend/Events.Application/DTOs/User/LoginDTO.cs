using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record LoginDTO
(
    [property: Required]
    string Email,

    [property: Required]
    string Password
);