using Events.Application.DTOs;
using Events.Application.Messaging;

namespace Events.Application.Queries;

public sealed record GetEventByIdQuery(Guid EventId) : IQuery<EventResponseDTO>;