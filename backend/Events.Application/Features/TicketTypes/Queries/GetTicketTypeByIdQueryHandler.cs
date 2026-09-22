using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Queries;

public sealed class GetTicketTypeByIdQueryHandler : IRequestHandler<GetTicketTypeByIdQuery, TicketTypeResponseDTO>
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly ResponseMapper _responseMapper;

    public GetTicketTypeByIdQueryHandler(
        ITicketTypeRepository ticketTypeRepository,
        ResponseMapper responseMapper)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _responseMapper = responseMapper;
    }

    public async Task<TicketTypeResponseDTO> Handle(GetTicketTypeByIdQuery query, CancellationToken ct = default)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(query.Id, ct)
            ?? throw new TicketTypeNotFoundException(query.Id);

        return _responseMapper.MapToResponse(ticketType);
    }
}
