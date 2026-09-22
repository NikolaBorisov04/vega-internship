using MediatR;

namespace Events.Application.Commands;

public sealed record DeleteUserCommand(Guid Id) : IRequest<string>;