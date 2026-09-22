using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetTicketByIdQuery(Guid Id) : IRequest<TicketResponseDTO>;
