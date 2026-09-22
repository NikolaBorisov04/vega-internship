using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record UpdateEventPhotoCommand(Guid Id, EventPhotoUpdateDTO dto) : IRequest<EventPhotoResponseDTO>;
