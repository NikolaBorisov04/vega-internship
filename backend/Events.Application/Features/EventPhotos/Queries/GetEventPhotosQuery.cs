using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetEventPhotosQuery() : IRequest<List<EventPhotoResponseDTO>>;
