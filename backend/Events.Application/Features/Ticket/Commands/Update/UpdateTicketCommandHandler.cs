using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketResponseDTO>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTicketCommandHandler(
        ITicketRepository ticketRepository,
        ITicketTypeRepository ticketTypeRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService)
    {
        _ticketRepository = ticketRepository;
        _ticketTypeRepository = ticketTypeRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
    }

    public async Task<TicketResponseDTO> Handle(UpdateTicketCommand command, CancellationToken ct)
    {
        var ticket = await _ticketRepository.GetByIdAsync(command.Id, ct)
            ?? throw new TicketNotFoundException(command.Id);

        if (!_currentUserService.IsAdmin)
        {
            var ticketType = await _ticketTypeRepository.GetByIdAsync(ticket.TicketTypeId, ct) ?? throw new TicketTypeNotFoundException(ticket.TicketTypeId);
            var organizerId = await _ticketTypeRepository.GetEventOrganizerIdAsync(ticketType.EventId, ct);
            
            if (organizerId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("Nemate dozvolu za izmenu ovog tiketa.");
            }
        }

        UpdateMapper.UpdateEntity(command.dto, ticket);

        _ticketRepository.Update(ticket);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(ticket);
    }
}
