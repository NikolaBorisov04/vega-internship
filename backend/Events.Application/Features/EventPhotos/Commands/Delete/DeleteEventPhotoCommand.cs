using MediatR;

namespace Events.Application.Commands;

public sealed record DeleteEventPhotoCommand(Guid Id) : IRequest<string>;
