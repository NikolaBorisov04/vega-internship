using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class DeleteEventSponsorshipCommandHandler : IRequestHandler<DeleteEventSponsorshipCommand, string>
{
    private readonly IEventSponsorshipRepository _eventSponsorshipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEventSponsorshipCommandHandler(
        IEventSponsorshipRepository eventSponsorshipRepository,
        IUnitOfWork unitOfWork)
    {
        _eventSponsorshipRepository = eventSponsorshipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(
        DeleteEventSponsorshipCommand command,
        CancellationToken ct)
    {
        var eventSponsorship = await _eventSponsorshipRepository.GetByIdAsync(command.Id, ct) ?? throw new EventSponsorshipNotFoundException(command.Id);
        _eventSponsorshipRepository.Delete(eventSponsorship);

        await _unitOfWork.SaveChangesAsync(ct);

        return $"Sponzorstvo sa ID-jem {command.Id} je uspesno izbrisano.";
    }
}
