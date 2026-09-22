using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetTicketTypesQueryHandler : IRequestHandler<GetTicketTypesQuery, List<TicketTypeResponseDTO>>
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly ResponseMapper _responseMapper;

    public GetTicketTypesQueryHandler(
        ITicketTypeRepository ticketTypeRepository,
        ResponseMapper responseMapper)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<TicketTypeResponseDTO>> Handle(GetTicketTypesQuery query, CancellationToken ct)
    {
        var ticketTypes = await _ticketTypeRepository.GetAllAsync(ct);
        if (ticketTypes is null || !ticketTypes.Any())
        {
            throw new KeyNotFoundException("Nema tipova tiketa u bazi.");
        }

        return ticketTypes.Select(_responseMapper.MapToResponse).ToList();
    }
}
