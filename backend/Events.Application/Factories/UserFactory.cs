using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Application.Commands;

namespace Events.Application.Factories;

public static class UserFactory
{
    public static User Create(CreateUserCommand command, string passwordHash)
    {
        return command.Role switch
        {
            UserRole.Customer => new Customer
            {
                Name = command.Name,
                Email = command.Email,
                PasswordHash = passwordHash,
                Role = UserRole.Customer,
                Country = command.County,
                City = command.City,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber
            },

            UserRole.Organizer => new Organizer
            {
                Name = command.Name,
                Email = command.Email,
                PasswordHash = passwordHash,
                Role = UserRole.Organizer,
                Country = command.County,
                City = command.City,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
                CompanyName = command.CompanyName,
                Validated = command.Validated ?? false
            },

            UserRole.Admin => new Admin
            {
                Name = command.Name,
                Email = command.Email,
                PasswordHash = passwordHash,
                Role = UserRole.Admin,
                Country = command.County,
                City = command.City,
                Address = command.Address,
                PhoneNumber = command.PhoneNumber,
                CompanyName = command.CompanyName,
                Validated = command.Validated ?? true
            },

            _ => throw new ArgumentException(
                $"Ne postoji role: {command.Role}")
        };
    }
}