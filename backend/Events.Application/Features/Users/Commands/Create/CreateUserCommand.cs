using Events.Application.DTOs;
using MediatR;
using Events.Domain.Enums;

namespace Events.Application.Commands;

public sealed record CreateUserCommand(
    string Name,
    string Email,
    UserRole Role,
    string Password,
    string? County = null,
    string? City = null,
    string? Address = null,
    string? PhoneNumber = null,
    string? CompanyName = null,
    bool? Validated = null
) : IRequest<UserResponseDTO>;