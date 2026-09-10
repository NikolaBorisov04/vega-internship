using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Security;
using Events.Api.Extensions;
using Events.Api.Mappings;

namespace Events.Api.Services;
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ResponseMapper _responseMapper;

    public UserService(ApplicationDbContext context, IPasswordHasher passwordHasher, ResponseMapper responseMapper)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _responseMapper = responseMapper;
    }
    public async Task<UserResponseDTO?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Where(u => u.Id == id)
            .ToUserResponseDTO()
            .FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
    {
        return await _context.Users
            .ToUserResponseDTO()
            .ToListAsync();
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

        return _responseMapper.MapToResponse(customer);
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

        return _responseMapper.MapToResponse(organizer);
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

        return _responseMapper.MapToResponse(admin);
    }

    public async Task<UserResponseDTO> ValidateUserAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return null;

        if(_passwordHasher.VerifyPassword(password, user.PasswordHash) == false)
            return null;
        
        return _responseMapper.MapToResponse(user);
    }

    private async Task EnsureEmailIsUniqueAsync(string email, CancellationToken ct)
    {
        var exists = await _context.Users.AnyAsync(u => u.Email == email, ct);
        if (exists)
        {
            throw new InvalidOperationException($"Korisnik sa email adresom '{email}' već postoji.");
        }
    }
}