using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateEventSponsorshipCommand(EventSponsorshipCreateDTO dto) : IRequest<EventSponsorshipResponseDTO>;
