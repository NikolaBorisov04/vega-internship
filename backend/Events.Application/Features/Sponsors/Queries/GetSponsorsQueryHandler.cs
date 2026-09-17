using Events.Application.Repositories;
using Events.Application.DTOs;
using Events.Application.Mappers;
using MediatR;
using Events.Domain.Exceptions;

namespace Events.Application.Queries;

public sealed class GetSponsorsQueryHandler : IRequestHandler<GetSponsorsQuery, List<SponsorResponseDTO>>
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly ResponseMapper _responseMapper;

    public GetSponsorsQueryHandler(
        ISponsorRepository sponsorRepository,
        ResponseMapper responseMapper)
    {
        _sponsorRepository = sponsorRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<SponsorResponseDTO>> Handle(
        GetSponsorsQuery query,
        CancellationToken ct)
    {
        var sponsors = await _sponsorRepository.GetAllAsync(ct) ?? throw new SponsorsNotFoundException();
        return sponsors.Select(_responseMapper.MapToResponse).ToList();
    }
}