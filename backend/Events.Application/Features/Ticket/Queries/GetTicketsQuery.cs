using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetTicketsQuery() : IRequest<List<TicketResponseDTO>>;
