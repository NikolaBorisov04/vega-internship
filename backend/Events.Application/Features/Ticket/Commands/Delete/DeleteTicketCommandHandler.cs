using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, string>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketCommandHandler(
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(DeleteTicketCommand command, CancellationToken ct)
    {
        var ticket = await _ticketRepository.GetByIdAsync(command.Id, ct)
            ?? throw new TicketNotFoundException(command.Id);

        _ticketRepository.Delete(ticket);
        await _unitOfWork.SaveChangesAsync(ct);

        return $"Tiketa sa ID-jem {command.Id} je uspesno izbrisan.";
    }
}
