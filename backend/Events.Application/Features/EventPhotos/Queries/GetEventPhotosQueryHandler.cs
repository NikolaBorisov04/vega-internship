using Events.Application.Repositories;
using Events.Application.DTOs;
using Events.Application.Mappers;
using MediatR;
using Events.Domain.Exceptions;

namespace Events.Application.Queries;

public sealed class GetEventPhotosQueryHandler : IRequestHandler<GetEventPhotosQuery, List<EventPhotoResponseDTO>>
{
    private readonly IEventPhotoRepository _eventPhotoRepository;
    private readonly ResponseMapper _responseMapper;

    public GetEventPhotosQueryHandler(
        IEventPhotoRepository eventPhotoRepository,
        ResponseMapper responseMapper)
    {
        _eventPhotoRepository = eventPhotoRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<EventPhotoResponseDTO>> Handle(
        GetEventPhotosQuery query,
        CancellationToken ct)
    {
        var eventPhotos = await _eventPhotoRepository.GetAllAsync(ct) ?? throw new EventPhotosNotFoundException();
        return eventPhotos.Select(_responseMapper.MapToResponse).ToList();
    }
}
