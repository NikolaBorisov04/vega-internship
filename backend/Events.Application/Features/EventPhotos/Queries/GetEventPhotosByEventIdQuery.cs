using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetEventPhotosByEventIdQuery(Guid EventId) : IRequest<List<EventPhotoResponseDTO>>;
