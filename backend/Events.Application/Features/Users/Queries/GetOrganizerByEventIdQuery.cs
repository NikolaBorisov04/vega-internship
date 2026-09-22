using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetOrganizerByEventIdQuery(Guid EventId) : IRequest<UserResponseDTO>;