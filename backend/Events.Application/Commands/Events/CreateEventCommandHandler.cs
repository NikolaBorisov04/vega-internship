using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Messaging;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Entities;

namespace Events.Application.Commands;

public sealed class CreateEventCommandHandler : ICommandHandler<CreateEventCommand, EventResponseDTO>
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
    public async Task<Result<EventResponseDTO>> Handle(CreateEventCommand command, CancellationToken ct)
    {
        var organizerId = _currentUserService.UserId;

        var newEvent = new Event
        {
            Title = command.Title,
            Description = command.Description,
            Country = command.Country,
            City = command.City,
            Address = command.Address,
            MainImageURL = command.MainImageURL,
            VenueName = command.VenueName,
            StartOfEvent = command.StartOfEvent,
            EndOfEvent = command.EndOfEvent,
            OrganizerId = organizerId
        };

        _eventRepository.Add(newEvent);

        await _unitOfWork.SaveChangesAsync(ct);

        var response = _responseMapper.MapToResponse(newEvent);

        return Result<EventResponseDTO>.Success(response);
    }
}