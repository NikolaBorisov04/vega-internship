using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetSponsorByIdQuery(Guid Id) : IRequest<SponsorResponseDTO>;