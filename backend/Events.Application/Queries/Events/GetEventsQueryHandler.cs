using Events.Application.Repositories;
using Events.Application.DTOs;
using Events.Application.Mappers;
using MediatR;
using Events.Domain.Exceptions;

namespace Events.Application.Queries;

public sealed class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<EventResponseDTO>>
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

    public async Task<List<EventResponseDTO>> Handle(
        GetEventsQuery query,
        CancellationToken ct)
    {
        var events = await _eventRepository.GetAllAsync(ct) ?? throw new EventsNotFoundException();
        return events.Select(_responseMapper.MapToResponse).ToList();
    }
}