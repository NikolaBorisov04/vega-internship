using Events.Application.Messaging;
using Events.Application.Repositories;
using Events.Application.DTOs;
using Events.Application.Mappers;

namespace Events.Application.Queries;

public class GetEventsQueryHandler : IQueryHandler<GetEventsQuery, List<EventResponseDTO>>
{
    private readonly IEventRepository _eventRepository;
    private readonly ResponseMapper _responseMapper;

    public GetEventsQueryHandler(
        IEventRepository eventRepository,
        ResponseMapper responseMapper)
    {
        _eventRepository = eventRepository;
        _responseMapper = responseMapper;
    }

    public async Task<Result<List<EventResponseDTO>>> Handle(
        GetEventsQuery query,
        CancellationToken ct)
    {
        var events = await _eventRepository.GetAllAsync(ct);

        var response = events.Select(_responseMapper.MapToResponse).ToList();

        return Result<List<EventResponseDTO>>.Success(response);
    }
}