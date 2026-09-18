using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<TicketResponseDTO>>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ResponseMapper _responseMapper;

    public GetTicketsQueryHandler(
        ITicketRepository ticketRepository,
        ResponseMapper responseMapper)
    {
        _ticketRepository = ticketRepository;
        _responseMapper = responseMapper;
    }

    public async Task<List<TicketResponseDTO>> Handle(GetTicketsQuery query, CancellationToken ct)
    {
        var tickets = await _ticketRepository.GetAllAsync(ct);
        if (tickets is null || !tickets.Any())
        {
            throw new TicketsNotFoundException();
        }

        return tickets.Select(_responseMapper.MapToResponse).ToList();
    }
}
