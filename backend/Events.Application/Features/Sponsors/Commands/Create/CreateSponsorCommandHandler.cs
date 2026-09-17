using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Application.Services;
using Events.Domain.Entities;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateSponsorCommandHandler : IRequestHandler<CreateSponsorCommand, SponsorResponseDTO>
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public CreateSponsorCommandHandler(
        ISponsorRepository sponsorRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper)
    {
        _sponsorRepository = sponsorRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }
    public async Task<SponsorResponseDTO> Handle(CreateSponsorCommand command, CancellationToken ct)
    {
        var newSponsor = new Sponsor
        {
            Name = command.dto.Name,
            ContactEmail = command.dto.ContactEmail,
            Description = command.dto.Description,
            WebsiteUrl = command.dto.WebsiteUrl,
            LogoUrl = command.dto.LogoUrl,
            TaxId = command.dto.TaxId
        };

        _sponsorRepository.Add(newSponsor);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(newSponsor);
    }
}