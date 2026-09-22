using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetSponsorsByEventIdQuery(Guid EventId) : IRequest<List<SponsorResponseDTO>>;
