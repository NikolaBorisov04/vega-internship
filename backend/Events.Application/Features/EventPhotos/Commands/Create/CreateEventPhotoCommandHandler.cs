using Events.Application.DTOs;
using Events.Application.Factories;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Entities;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateEventPhotoCommandHandler : IRequestHandler<CreateEventPhotoCommand, EventPhotoResponseDTO>
{
    private readonly IEventPhotoRepository _eventPhotoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public CreateEventPhotoCommandHandler(
        IEventPhotoRepository eventPhotoRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper)
    {
        _eventPhotoRepository = eventPhotoRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }

    public async Task<EventPhotoResponseDTO> Handle(CreateEventPhotoCommand command, CancellationToken ct)
    {
        var newEventPhoto = EventPhotoFactory.Create(command);

        _eventPhotoRepository.Add(newEventPhoto);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(newEventPhoto);
    }
}
