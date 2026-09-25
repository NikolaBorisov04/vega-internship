using Events.Application.DTOs;
using Events.Application.Storage;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateEventPhotoCommand(EventPhotoCreateDTO Dto, FileUpload Image) : IRequest<EventPhotoResponseDTO>;
