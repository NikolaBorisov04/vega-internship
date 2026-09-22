using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateTicketCommand(TicketCreateDTO dto) : IRequest<TicketResponseDTO>;
