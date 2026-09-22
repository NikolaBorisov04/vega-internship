using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Application.Storage;
using Events.Domain.Entities;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventResponseDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    private readonly ResponseMapper _responseMapper;
    private readonly IImageStorage _imageStorage;

    public CreateEventCommandHandler(
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService,
        ResponseMapper responseMapper,
        IImageStorage imageStorage)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
        _responseMapper = responseMapper;
        _imageStorage = imageStorage;
    }
    public async Task<EventResponseDTO> Handle(CreateEventCommand command, CancellationToken ct)
    {
        var organizerId = _currentUserService.UserId;

        var image = await _imageStorage.UploadAsync(
            command.MainImage.Stream,
            command.MainImage.FileName,
            ct);

        try
        {
            var newEvent = EventFactory.Create(
                command,
                organizerId,
                image.Url,
                image.PublicId);

            _eventRepository.Add(newEvent);

            await _unitOfWork.SaveChangesAsync(ct);

            return _responseMapper.MapToResponse(newEvent);
        }
        catch
        {
            await _imageStorage.DeleteAsync(
                image.PublicId,
                ct);

            throw new ArgumentException("Dogadjaj nije uspesno napravljen.");
        }
    }
}