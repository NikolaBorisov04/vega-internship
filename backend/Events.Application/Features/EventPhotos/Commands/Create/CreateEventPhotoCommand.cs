using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateEventPhotoCommand(EventPhotoCreateDTO dto) : IRequest<EventPhotoResponseDTO>;
