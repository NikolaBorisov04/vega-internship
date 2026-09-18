using EventPhotos.Application.Commands;
using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class UpdateEventPhotoCommandHandler : IRequestHandler<UpdateEventPhotoCommand, EventPhotoResponseDTO>
{
    private readonly IEventPhotoRepository _eventPhotoRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;

    public UpdateEventPhotoCommandHandler(
        IEventPhotoRepository eventPhotoRepository,
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        ICurrentUserService currentUserService)
    {
        _eventPhotoRepository = eventPhotoRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
    }

    public async Task<EventPhotoResponseDTO> Handle(UpdateEventPhotoCommand command, CancellationToken ct)
    {
        var eventPhoto = await _eventPhotoRepository.GetByIdAsync(command.Id, ct)
            ?? throw new EventPhotoNotFoundException(command.Id);

        if (!_currentUserService.IsAdmin)
        {
            var organizerId = await _eventRepository.GetEventOrganizerIdAsync(eventPhoto.EventId, ct);

            if (organizerId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("Nemate dozvolu za izmenu ove slike.");
        }

        UpdateMapper.UpdateEntity(command.dto, eventPhoto);

        _eventPhotoRepository.Update(eventPhoto);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(eventPhoto);
    }
}
