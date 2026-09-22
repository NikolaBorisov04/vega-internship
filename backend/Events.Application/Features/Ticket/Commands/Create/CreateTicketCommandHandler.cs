using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Entities;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketResponseDTO>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITicketTypeRepository _ticketTypeRepository;

    public CreateTicketCommandHandler(
        ITicketRepository ticketRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService,
        ITicketTypeRepository ticketTypeRepository)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
        _ticketTypeRepository = ticketTypeRepository;
    }

    public async Task<TicketResponseDTO> Handle(CreateTicketCommand command, CancellationToken ct)
    {
        var ticketTypeExists = await _ticketTypeRepository.ExistsAsync(command.dto.TicketTypeId, ct);
        if (!ticketTypeExists)
        {
            throw new TicketTypeNotFoundException(command.dto.TicketTypeId);
        }
        
        var ticket = TicketFactory.Create(command, _currentUserService.UserId);

        _ticketRepository.Add(ticket);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(ticket);
    }
}
