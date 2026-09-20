using Events.Application.DTOs;
using Events.Application.Extensions;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class UpdateTicketTypeCommandHandler : IRequestHandler<UpdateTicketTypeCommand, TicketTypeResponseDTO>
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTicketTypeCommandHandler(
        ITicketTypeRepository ticketTypeRepository,
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
    }

    public async Task<TicketTypeResponseDTO> Handle(UpdateTicketTypeCommand command, CancellationToken ct)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(command.Id, ct)
            ?? throw new TicketTypeNotFoundException(command.Id);

        if (!_currentUserService.IsAdmin)
        {
            var organizerId = await _eventRepository.GetEventOrganizerIdAsync(ticketType.EventId, ct);
            if (organizerId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("Nemate dozvolu za izmenu ovog tipa tiketa.");
            }
        }

        ticketType.UpdateFrom(command.dto);

        _ticketTypeRepository.Update(ticketType);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(ticketType);
    }
}
