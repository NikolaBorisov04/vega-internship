using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, string>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEventCommandHandler(
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(DeleteEventCommand command, CancellationToken ct)
    {
        var _event = await _eventRepository.GetByIdAsync(command.Id, ct)
            ?? throw new EventNotFoundException(command.Id);

        _eventRepository.Delete(_event);
        await _unitOfWork.SaveChangesAsync(ct);

        return $"Dogadjaj sa ID-jem {command.Id} je uspesno izbrisan.";
    }
}
