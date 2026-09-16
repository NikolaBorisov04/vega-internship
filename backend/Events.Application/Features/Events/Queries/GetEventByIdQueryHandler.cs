using Events.Application.DTOs;
using Events.Application.Repositories;
using Events.Application.Mappers;
using MediatR;
using Events.Domain.Exceptions;

namespace Events.Application.Queries;
public sealed class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventResponseDTO>
{
    private readonly IEventRepository _eventRepository;
    private readonly ResponseMapper _responseMapper;

    public GetEventByIdQueryHandler(IEventRepository eventRepository, ResponseMapper responseMapper)
    {
        _eventRepository = eventRepository;
        _responseMapper = responseMapper;
    }

    public async Task<EventResponseDTO> Handle(
        GetEventByIdQuery query,
        CancellationToken ct)
    {
        var eventEntity =
            await _eventRepository.GetByIdAsync(
                query.Id,
                ct);

        if (eventEntity is null)
        {
            throw new EventNotFoundException(query.Id);
        }

        return _responseMapper.MapToResponse(eventEntity);
    }
}