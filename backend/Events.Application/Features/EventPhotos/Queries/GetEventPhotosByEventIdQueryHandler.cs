using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetEventPhotosByEventIdQueryHandler : IRequestHandler<GetEventPhotosByEventIdQuery, List<EventPhotoResponseDTO>>
{
    private readonly IEventPhotoRepository _eventPhotoRepository;
    private readonly ResponseMapper _responseMapper;

    public GetEventPhotosByEventIdQueryHandler(
        IEventPhotoRepository eventPhotoRepository,
        ResponseMapper responseMapper)
    {
        _eventPhotoRepository = eventPhotoRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<EventPhotoResponseDTO>> Handle(GetEventPhotosByEventIdQuery query, CancellationToken ct)
    {
        var eventPhotos = await _eventPhotoRepository.GetByEventIdAsync(query.EventId, ct);
        if (eventPhotos is null || !eventPhotos.Any())
        {
            throw new NoEventPhotosForEventIdException(query.EventId);
        }

        return eventPhotos.Select(_responseMapper.MapToResponse).ToList();
    }
}
