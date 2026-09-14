using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Mappings;
using Events.Api.IRepositories;
using Events.Api.Security;

namespace Events.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ResponseMapper _responseMapper;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ResponseMapper responseMapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _responseMapper = responseMapper;
    }

    public async Task<UserResponseDTO?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if(user == null)
        {
            return null;
        }

        return _responseMapper.MapToResponse(user);
    }

    public async Task<IEnumerable<UserResponseDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(_responseMapper.MapToResponse).ToList();
    }

    public async Task<UserResponseDTO> RegisterCustomerAsync(RegisterCustomerDTO dto, CancellationToken ct = default)
    {
        await EnsureEmailIsUniqueAsync(dto.Email, ct);

        //Ovo nekad trebam da promenim da ga extraktujem i da imam posle samo organizer.ToCustomer() illi nesto slicno
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

        _userRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(customer);
    }

    public async Task<UserResponseDTO> RegisterOrganizerAsync(
        RegisterOrganizerDTO dto,
        CancellationToken ct = default)
    {
        await EnsureEmailIsUniqueAsync(dto.Email, ct);

        //I ovde isto
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
            Validated = false
        };

        _userRepository.Add(organizer);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(organizer);
    }

    public async Task<UserResponseDTO> RegisterAdminAsync(RegisterAdminDTO dto, CancellationToken ct = default)
    {
        await EnsureEmailIsUniqueAsync(dto.Email, ct);

        //I ovde isto
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
            Validated = true
        };

        _userRepository.Add(admin);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(admin);
    }

    public async Task<UserResponseDTO> ValidateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
        {
            return null;
        }

        return _responseMapper.MapToResponse(user);
    }

    private async Task EnsureEmailIsUniqueAsync(string email, CancellationToken ct)
    {
        var exists = await _userRepository.EmailExistsAsync(email, ct);

        if (exists)
        {
            throw new InvalidOperationException($"Korisnik sa email adresom '{email}' već postoji.");
        }
    }
}