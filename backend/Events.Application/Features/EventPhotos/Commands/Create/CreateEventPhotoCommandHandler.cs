using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Application.Storage;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateEventPhotoCommandHandler : IRequestHandler<CreateEventPhotoCommand, EventPhotoResponseDTO>
{
    private readonly IEventPhotoRepository _eventPhotoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly IImageStorage _imageStorage;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEventRepository _eventRepository;

    public CreateEventPhotoCommandHandler(
        IEventPhotoRepository eventPhotoRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper,
        IImageStorage imageStorage,
        ICurrentUserService currentUserService,
        IEventRepository eventRepository)
    {
        _eventPhotoRepository = eventPhotoRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _imageStorage = imageStorage;
        _currentUserService = currentUserService;
        _eventRepository = eventRepository;
    }

    public async Task<EventPhotoResponseDTO> Handle(CreateEventPhotoCommand command, CancellationToken ct)
    {
        var organizerId = await _eventRepository.GetEventOrganizerIdAsync(command.Dto.EventId, ct);

        if(!_currentUserService.IsAdmin)
        {
            if(_currentUserService.UserId != organizerId)
            {
                throw new UnauthorizedAccessException("You cannot upload photos for the event you haven't previously created.");
            }
        }

        var image = await _imageStorage.UploadAsync(command.Image.Stream, command.Image.FileName, ct);

        try
        {
            var newEventPhoto = EventPhotoFactory.Create(command, image.Url, image.PublicId);

            _eventPhotoRepository.Add(newEventPhoto);

            await _unitOfWork.SaveChangesAsync(ct);

            return _responseMapper.MapToResponse(newEventPhoto);
        }
        catch
        {
            await _imageStorage.DeleteAsync(image.PublicId, ct);

            throw new ArgumentException("Event photo wasn't created succesfully.");
        }
    }
}
