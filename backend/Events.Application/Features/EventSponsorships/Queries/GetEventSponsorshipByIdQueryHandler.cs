using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetEventSponsorshipByIdQueryHandler : IRequestHandler<GetEventSponsorshipByIdQuery, EventSponsorshipResponseDTO>
{
    private readonly ResponseMapper _responseMapper;
    private readonly IEventSponsorshipRepository _eventSponsorshipRepository;

    public GetEventSponsorshipByIdQueryHandler(
        ResponseMapper responseMapper,
        IEventSponsorshipRepository eventSponsorshipRepository)
    {
        _responseMapper = responseMapper;
        _eventSponsorshipRepository = eventSponsorshipRepository;
    }

    public async Task<EventSponsorshipResponseDTO> Handle(GetEventSponsorshipByIdQuery query, CancellationToken ct = default)
    {
        var result = await _eventSponsorshipRepository.GetByIdAsync(query.Id, ct) ?? throw new EventSponsorshipNotFoundException(query.Id);
        return _responseMapper.MapToResponse(result);
    }
}
