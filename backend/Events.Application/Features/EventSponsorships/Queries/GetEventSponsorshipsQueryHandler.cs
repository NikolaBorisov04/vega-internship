using Events.Application.Repositories;
using Events.Application.DTOs;
using Events.Application.Mappers;
using MediatR;
using Events.Domain.Exceptions;

namespace Events.Application.Queries;

public sealed class GetEventSponsorshipsQueryHandler : IRequestHandler<GetEventSponsorshipsQuery, List<EventSponsorshipResponseDTO>>
{
    private readonly IEventSponsorshipRepository _eventSponsorshipRepository;
    private readonly ResponseMapper _responseMapper;

    public GetEventSponsorshipsQueryHandler(
        IEventSponsorshipRepository eventSponsorshipRepository,
        ResponseMapper responseMapper)
    {
        _eventSponsorshipRepository = eventSponsorshipRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<EventSponsorshipResponseDTO>> Handle(
        GetEventSponsorshipsQuery query,
        CancellationToken ct)
    {
        var sponsorships = await _eventSponsorshipRepository.GetAllAsync(ct) ?? throw new EventSponsorshipsNotFoundException();
        return sponsorships.Select(_responseMapper.MapToResponse).ToList();
    }
}
