using MediatR;

namespace Events.Application.Commands;

public sealed record DeleteSponsorCommand(Guid Id) : IRequest<string>;