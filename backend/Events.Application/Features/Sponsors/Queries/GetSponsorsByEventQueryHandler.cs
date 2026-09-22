using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetSponsorsByEventIdQueryHandler : IRequestHandler<GetSponsorsByEventIdQuery, List<SponsorResponseDTO>>
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly ResponseMapper _responseMapper;

    public GetSponsorsByEventIdQueryHandler(
        ISponsorRepository sponsorRepository,
        ResponseMapper responseMapper)
    {
        _sponsorRepository = sponsorRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<SponsorResponseDTO>> Handle(GetSponsorsByEventIdQuery query, CancellationToken ct)
    {
        var sponsors = await _sponsorRepository.GetByEventIdAsync(query.EventId, ct);
        if (sponsors is null || !sponsors.Any())
        {
            throw new NoSponsorsForEventIdException(query.EventId);
        }

        return sponsors.Select(_responseMapper.MapToResponse).ToList();
    }
}
