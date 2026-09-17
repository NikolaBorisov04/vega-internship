using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetEventByIdQuery(string Id) : IRequest<EventResponseDTO>;