using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record UpdateSponsorCommand(Guid Id, SponsorUpdateDTO Dto) : IRequest<SponsorResponseDTO>;
