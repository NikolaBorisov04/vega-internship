using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetEventPhotoByIdQueryHandler : IRequestHandler<GetEventPhotoByIdQuery, EventPhotoResponseDTO>
{
    private readonly ResponseMapper _responseMapper;
    private readonly IEventPhotoRepository _eventPhotoRepository;

    public GetEventPhotoByIdQueryHandler(
        ResponseMapper responseMapper,
        IEventPhotoRepository eventPhotoRepository)
    {
        _responseMapper = responseMapper;
        _eventPhotoRepository = eventPhotoRepository;
    }

    public async Task<EventPhotoResponseDTO> Handle(GetEventPhotoByIdQuery query, CancellationToken ct = default)
    {
        var result = await _eventPhotoRepository.GetByIdAsync(query.Id, ct) ?? throw new EventPhotoNotFoundException(query.Id);
        return _responseMapper.MapToResponse(result);
    }
}
