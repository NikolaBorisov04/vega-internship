using Events.Application.DTOs;
using Events.Application.Messaging;

namespace Events.Application.Queries;

public record GetEventsQuery : IQuery<List<EventResponseDTO>>;