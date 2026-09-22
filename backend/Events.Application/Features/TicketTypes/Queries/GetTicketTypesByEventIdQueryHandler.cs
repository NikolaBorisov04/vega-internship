using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetTicketTypesByEventIdQueryHandler : IRequestHandler<GetTicketTypesByEventIdQuery, List<TicketTypeResponseDTO>>
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly ResponseMapper _responseMapper;

    public GetTicketTypesByEventIdQueryHandler(
        ITicketTypeRepository ticketTypeRepository,
        ResponseMapper responseMapper)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<TicketTypeResponseDTO>> Handle(GetTicketTypesByEventIdQuery query, CancellationToken ct)
    {
        var ticketTypes = await _ticketTypeRepository.GetByEventIdAsync(query.EventId, ct);
        if (ticketTypes is null || !ticketTypes.Any())
        {
            throw new NoTicketTypesForEventIdException(query.EventId);
        }

        return ticketTypes.Select(_responseMapper.MapToResponse).ToList();
    }
}
