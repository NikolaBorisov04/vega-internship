using Events.Application.DTOs;
using Events.Domain.Entities;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Security;
using Events.Domain.Enums;

namespace Events.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ResponseMapper _responseMapper;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ResponseMapper responseMapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _responseMapper = responseMapper;
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
}