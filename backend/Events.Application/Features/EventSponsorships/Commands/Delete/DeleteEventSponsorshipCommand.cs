using MediatR;

namespace Events.Application.Commands;

public sealed record DeleteEventSponsorshipCommand(Guid Id) : IRequest<string>;
