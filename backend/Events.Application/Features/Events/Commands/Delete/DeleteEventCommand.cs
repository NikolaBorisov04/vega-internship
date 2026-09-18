using MediatR;

namespace Events.Application.Commands;

public sealed record DeleteEventCommand(Guid Id) : IRequest<string>;
