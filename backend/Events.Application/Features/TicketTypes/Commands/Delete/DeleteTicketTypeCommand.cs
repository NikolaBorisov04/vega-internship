using MediatR;

namespace Events.Application.Commands;

public sealed record DeleteTicketTypeCommand(Guid Id) : IRequest<string>;
