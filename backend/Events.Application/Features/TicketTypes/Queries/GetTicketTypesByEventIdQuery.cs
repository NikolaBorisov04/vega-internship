using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetTicketTypesByEventIdQuery(Guid EventId) : IRequest<List<TicketTypeResponseDTO>>;
