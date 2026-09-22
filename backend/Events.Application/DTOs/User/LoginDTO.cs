using System.ComponentModel.DataAnnotations;

namespace Events.Application.DTOs;

public sealed record LoginDTO
(
    [param: Required]
    string Email,

    [param: Required]
    string Password
);