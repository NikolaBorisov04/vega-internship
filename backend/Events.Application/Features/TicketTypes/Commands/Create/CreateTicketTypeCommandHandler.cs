using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Entities;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateTicketTypeCommandHandler : IRequestHandler<CreateTicketTypeCommand, TicketTypeResponseDTO>
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateTicketTypeCommandHandler(
        ITicketTypeRepository ticketTypeRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
    }

    public async Task<TicketTypeResponseDTO> Handle(CreateTicketTypeCommand command, CancellationToken ct)
    {
        var eventExists = await _ticketTypeRepository.EventExistsAsync(command.dto.EventId, ct);
        if (!eventExists)
        {
            throw new EventNotFoundException(command.dto.EventId);
        }

        if (!_currentUserService.IsAdmin)
        {
            var organizerId = await _ticketTypeRepository.GetEventOrganizerIdAsync(command.dto.EventId, ct);
            if (organizerId != _currentUserService.UserId)
            {
                throw new UnauthorizedAccessException("Nemate dozvolu za kreiranje tipa tiketa za ovaj dogadjaj.");
            }
        }
        
        var ticketType = TicketTypeFactory.Create(command);

        _ticketTypeRepository.Add(ticketType);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(ticketType);
    }
}
