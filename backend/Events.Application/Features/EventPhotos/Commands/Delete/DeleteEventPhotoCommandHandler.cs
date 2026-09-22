using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class DeleteEventPhotoCommandHandler : IRequestHandler<DeleteEventPhotoCommand, string>
{
    private readonly IEventPhotoRepository _eventPhotoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEventPhotoCommandHandler(
        IEventPhotoRepository eventPhotoRepository,
        IUnitOfWork unitOfWork)
    {
        _eventPhotoRepository = eventPhotoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(
        DeleteEventPhotoCommand command,
        CancellationToken ct)
    {
        var eventPhoto = await _eventPhotoRepository.GetByIdAsync(command.Id, ct) ?? throw new EventPhotoNotFoundException(command.Id);
        _eventPhotoRepository.Delete(eventPhoto);

        await _unitOfWork.SaveChangesAsync(ct);

        return $"Slika sa ID-jem {command.Id} je uspesno izbrisana.";
    }
}
