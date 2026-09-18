using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record UpdateEventCommand(Guid Id, EventUpdateDTO dto) : IRequest<EventResponseDTO>;
