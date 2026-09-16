using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public record GetEventsQuery : IRequest<List<EventResponseDTO>>;