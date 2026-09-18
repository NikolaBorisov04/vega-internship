using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class UpdateEventSponsorshipCommandHandler : IRequestHandler<UpdateEventSponsorshipCommand, EventSponsorshipResponseDTO>
{
    private readonly IEventSponsorshipRepository _eventSponsorshipRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateEventSponsorshipCommandHandler(
        IEventSponsorshipRepository eventSponsorshipRepository,
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService)
    {
        _eventSponsorshipRepository = eventSponsorshipRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
    }

    public async Task<EventSponsorshipResponseDTO> Handle(UpdateEventSponsorshipCommand command, CancellationToken ct)
    {
        var eventsponsorship = await _eventSponsorshipRepository.GetByIdAsync(command.Id, ct)
            ?? throw new EventSponsorshipNotFoundException(command.Id);


        UpdateMapper.UpdateEntity(command.Dto, eventsponsorship);

        _eventSponsorshipRepository.Update(eventsponsorship);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(eventsponsorship);
    }
}
