using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;
public sealed class GetSponsorByIdQueryHandler : IRequestHandler<GetSponsorByIdQuery, SponsorResponseDTO>
{
    private readonly ResponseMapper _responseMapper;
    private readonly ISponsorRepository _sponsorRepository;
    public GetSponsorByIdQueryHandler(
        ResponseMapper responseMapper,
        ISponsorRepository sponsorRepository
    )
    {
        _responseMapper = responseMapper;
        _sponsorRepository = sponsorRepository;
    }

    public async Task<SponsorResponseDTO> Handle(GetSponsorByIdQuery query, CancellationToken ct = default)
    {
        var result = await _sponsorRepository.GetByIdAsync(query.Id) ?? throw new SponsorNotFoundException(query.Id);
        return _responseMapper.MapToResponse(result);
    }
}