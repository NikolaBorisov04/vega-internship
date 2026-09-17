using Events.Application.DTOs;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateSponsorCommand(SponsorCreateDTO dto) : IRequest<SponsorResponseDTO>;