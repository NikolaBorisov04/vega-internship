using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Security;

namespace Events.Api.Services;
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<UserResponseDTO?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserResponseDTO(
                u.Id,
                u.Name,
                u.Email,
                u.Role,
                u is Organizer ? ((Organizer)u).CompanyName : null,
                u is Organizer ? ((Organizer)u).Validated : (bool?)null
            ))
            .FirstOrDefaultAsync();
    }
    public async Task<UserResponseDTO> RegisterCustomerAsync(RegisterCustomerDTO dto, CancellationToken ct = default)
    {
        await EnsureEmailIsUniqueAsync(dto.Email, ct);

        var customer = new Customer
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            Role = UserRole.Customer,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(ct);

        return MapToResponse(customer);
    }
    public async Task<UserResponseDTO> RegisterOrganizerAsync(RegisterOrganizerDTO dto, CancellationToken ct = default)
    {
        await EnsureEmailIsUniqueAsync(dto.Email, ct);

        var organizer = new Organizer
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            Role = UserRole.Organizer,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            CompanyName = dto.CompanyName,
            Validated = false // Zahteva odobrenje admina
        };

        _context.Organizers.Add(organizer);
        await _context.SaveChangesAsync(ct);

        return MapToResponse(organizer);
    }
    public async Task<UserResponseDTO> RegisterAdminAsync(RegisterAdminDTO dto, CancellationToken ct = default)
    {
        await EnsureEmailIsUniqueAsync(dto.Email, ct);

        var admin = new Admin
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            Role = UserRole.Admin,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            CompanyName = dto.CompanyName,
            Validated = true // Admin je po defaultu moze da organizuje evente
        };

        _context.Admins.Add(admin);
        await _context.SaveChangesAsync(ct);

        return MapToResponse(admin);
    }
    private async Task EnsureEmailIsUniqueAsync(string email, CancellationToken ct)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == email, ct);
        if (exists)
        {
            throw new InvalidOperationException($"Korisnik sa email adresom '{email}' već postoji.");
        }
    }

    private static UserResponseDTO MapToResponse(User user)
    {
        return user switch
        {
            Admin admin => new UserResponseDTO(admin.Id, admin.Name, admin.Email, admin.Role, admin.CompanyName, admin.Validated),
            Organizer organizer => new UserResponseDTO(organizer.Id, organizer.Name, organizer.Email, organizer.Role, organizer.CompanyName, organizer.Validated),
            Customer customer => new UserResponseDTO(customer.Id, customer.Name, customer.Email, customer.Role),
            _ => new UserResponseDTO(user.Id, user.Name, user.Email, user.Role)
        };
    }
}