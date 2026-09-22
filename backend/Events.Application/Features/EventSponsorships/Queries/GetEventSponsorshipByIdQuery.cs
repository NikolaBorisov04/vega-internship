using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetEventSponsorshipByIdQuery(Guid Id) : IRequest<EventSponsorshipResponseDTO>;
