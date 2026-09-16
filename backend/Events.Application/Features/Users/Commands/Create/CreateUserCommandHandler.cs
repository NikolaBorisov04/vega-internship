using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Security;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, UserResponseDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ResponseMapper _responseMapper;

    public CreateUserCommandHandler(
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

    public async Task<UserResponseDTO> Handle(
        CreateUserCommand command,
        CancellationToken ct)
    {
        var emailExists =
            await _userRepository.EmailExistsAsync(command.Email, ct);

        if (emailExists)
        {
            throw new InvalidOperationException(
                $"Korisnik sa email adresom '{command.Email}' već postoji.");
        }

        var passwordHash =
            _passwordHasher.HashPassword(command.Password);

        var user = UserFactory.Create(command, passwordHash);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(user);
    }
}