using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Entities;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventResponseDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly ResponseMapper _responseMapper;

    public CreateEventCommandHandler(
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        ResponseMapper responseMapper)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _responseMapper = responseMapper;
    }
    public async Task<EventResponseDTO> Handle(CreateEventCommand command, CancellationToken ct)
    {
        var organizerId = _currentUserService.UserId;

        var newEvent = EventFactory.Create(command, organizerId);

        _eventRepository.Add(newEvent);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(newEvent);
    }
}