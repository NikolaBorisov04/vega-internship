using Events.Application.DTOs;
using Events.Application.Storage;
using MediatR;

namespace Events.Application.Commands;

public sealed record CreateSponsorCommand(SponsorCreateDTO Dto, FileUpload Image) : IRequest<SponsorResponseDTO>;