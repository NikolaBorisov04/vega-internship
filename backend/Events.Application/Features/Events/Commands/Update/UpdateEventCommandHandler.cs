using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventResponseDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateEventCommandHandler(
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
    }

    public async Task<EventResponseDTO> Handle(UpdateEventCommand command, CancellationToken ct)
    {
        var _event = await _eventRepository.GetByIdAsync(command.Id, ct)
            ?? throw new EventNotFoundException(command.Id);

        if (!_currentUserService.IsAdmin)
        {
            var organizerExists = await _eventRepository.OrganizerExistsAsync(_event.OrganizerId, ct);
            if(!organizerExists)
                throw new UserNotFoundException(_event.OrganizerId);

            if (_event.OrganizerId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("Nemate dozvolu za izmenu ovog dogadjaja.");
        }

        UpdateMapper.UpdateEntity(command.dto, _event);

        _eventRepository.Update(_event);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(_event);
    }
}
