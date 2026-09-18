using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class DeleteTicketTypeCommandHandler : IRequestHandler<DeleteTicketTypeCommand, string>
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteTicketTypeCommandHandler(
        ITicketTypeRepository ticketTypeRepository,
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<string> Handle(DeleteTicketTypeCommand command, CancellationToken ct)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(command.Id, ct)
            ?? throw new TicketTypeNotFoundException(command.Id);

        if (!_currentUserService.IsAdmin)
        {
            var organizerId = await _eventRepository.GetEventOrganizerIdAsync(ticketType.EventId, ct);
            if (organizerId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("Nemate dozvolu za brisanje ovog tipa tiketa.");
            }
        }

        _ticketTypeRepository.Delete(ticketType);
        await _unitOfWork.SaveChangesAsync(ct);

        return $"Tip tiketa sa ID-jem {command.Id} je uspesno izbrisan.";
    }
}
