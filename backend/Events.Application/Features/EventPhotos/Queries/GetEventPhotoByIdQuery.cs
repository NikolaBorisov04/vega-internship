using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetEventPhotoByIdQuery(Guid Id) : IRequest<EventPhotoResponseDTO>;
