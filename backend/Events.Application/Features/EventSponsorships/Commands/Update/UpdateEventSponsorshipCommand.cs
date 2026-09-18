using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record UpdateEventSponsorshipCommand(Guid Id, EventSponsorshipUpdateDTO Dto) : IRequest<EventSponsorshipResponseDTO>;
