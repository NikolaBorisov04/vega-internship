using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(
        DeleteUserCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(command.Id, ct) ?? throw new UserNotFoundException(command.Id);
        _userRepository.Delete(user);

        await _unitOfWork.SaveChangesAsync(ct);

        return $"Korisnik sa ID-jem {command.Id} je uspeno izbrisan.";
    }
}