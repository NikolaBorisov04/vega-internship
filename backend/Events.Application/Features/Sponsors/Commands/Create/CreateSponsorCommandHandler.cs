using Events.Application.DTOs;
using Events.Application.Factories;
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
        var newSponsor = SponsorFactory.Create(command);

        _sponsorRepository.Add(newSponsor);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(newSponsor);
    }
}