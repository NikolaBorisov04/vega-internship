using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record UpdateTicketCommand(Guid Id, TicketUpdateDTO dto) : IRequest<TicketResponseDTO>;
