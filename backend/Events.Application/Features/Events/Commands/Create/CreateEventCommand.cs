using Events.Application.DTOs;
using Events.Application.Storage;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateEventCommand(EventCreateDTO dto, FileUpload MainImage) : IRequest<EventResponseDTO>;