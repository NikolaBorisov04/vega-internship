using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketResponseDTO>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly ResponseMapper _responseMapper;

    public GetTicketByIdQueryHandler(
        ITicketRepository ticketRepository,
        ResponseMapper responseMapper)
    {
        _ticketRepository = ticketRepository;
        _responseMapper = responseMapper;
    }

    public async Task<TicketResponseDTO> Handle(GetTicketByIdQuery query, CancellationToken ct = default)
    {
        var ticket = await _ticketRepository.GetByIdAsync(query.Id, ct)
            ?? throw new TicketNotFoundException(query.Id);

        return _responseMapper.MapToResponse(ticket);
    }
}
