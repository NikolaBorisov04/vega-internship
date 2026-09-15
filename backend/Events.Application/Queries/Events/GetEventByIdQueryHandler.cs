using Events.Application.DTOs;
using Events.Application.Repositories;
using Events.Application.Messaging;
using Events.Application.Mappers;

namespace Events.Application.Queries;
public sealed class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery, EventResponseDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly ResponseMapper _responseMapper;

    public GetEventByIdQueryHandler(IEventRepository eventRepository, ResponseMapper responseMapper)
    {
        _eventRepository = eventRepository;
        _responseMapper = responseMapper;
    }

    public async Task<Result<EventResponseDTO>> Handle(
        GetEventByIdQuery query,
        CancellationToken ct)
    {
        var eventEntity =
            await _eventRepository.GetByIdAsync(
                query.EventId,
                ct);

        if (eventEntity is null)
        {
            return Result<EventResponseDTO>.Failure(
                "Event not found.");
        }

        var response = _responseMapper.MapToResponse(eventEntity);

        return Result<EventResponseDTO>.Success(response);
    }
}