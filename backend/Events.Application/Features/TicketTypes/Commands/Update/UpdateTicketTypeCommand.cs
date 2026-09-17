using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record UpdateTicketTypeCommand(Guid Id, TicketTypeUpdateDTO dto) : IRequest<TicketTypeResponseDTO>;
