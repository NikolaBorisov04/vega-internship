using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Queries;

public sealed record GetEventSponsorshipsQuery() : IRequest<List<EventSponsorshipResponseDTO>>;
