using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Security;
using Events.Application.Services;
using MediatR;

namespace Events.Application.Commands;

public sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, LoginResultDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly ResponseMapper _responseMapper;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        ResponseMapper responseMapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _responseMapper = responseMapper;
    }

    public async Task<LoginResultDTO> Handle(
        LoginCommand request,
        CancellationToken ct)
    {
        var user = await _userRepository
            .GetByEmailAsync(request.Dto.Email, ct);

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Neispravan email ili lozinka.");
        }

        var passwordValid = _passwordHasher.VerifyPassword(
            request.Dto.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var userResponse = _responseMapper.MapToResponse(user);

        var token = _tokenService.GenerateJwtToken(userResponse);

        return new LoginResultDTO(
            userResponse,
            token);
    }
}