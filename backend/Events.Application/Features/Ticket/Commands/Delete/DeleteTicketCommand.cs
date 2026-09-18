using MediatR;

namespace Events.Application.Commands;

public sealed record DeleteTicketCommand(Guid Id) : IRequest<string>;
