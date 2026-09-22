using Events.Application.DTOs;
using Events.Application.Extensions;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Exceptions;
using MediatR;

namespace Events.Application.Commands;

public sealed class UpdateSponsorCommandHandler : IRequestHandler<UpdateSponsorCommand, SponsorResponseDTO>
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public UpdateSponsorCommandHandler(
        ISponsorRepository sponsorRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper)
    {
        _sponsorRepository = sponsorRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }

    public async Task<SponsorResponseDTO> Handle(UpdateSponsorCommand command, CancellationToken ct)
    {
        var eventsponsorship = await _sponsorRepository.GetByIdAsync(command.Id, ct)
            ?? throw new SponsorNotFoundException(command.Id);


        eventsponsorship.UpdateFrom(command.Dto);

        _sponsorRepository.Update(eventsponsorship);
        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(eventsponsorship);
    }
}
