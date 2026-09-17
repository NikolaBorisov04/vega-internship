using Events.Application.DTOs;
using Events.Application.Mappers;
using Events.Application.Repositories;
using Events.Domain.Entities;
using MediatR;

namespace Events.Application.Commands;

public sealed class CreateEventSponsorshipCommandHandler : IRequestHandler<CreateEventSponsorshipCommand, EventSponsorshipResponseDTO>
{
    private readonly IEventSponsorshipRepository _eventSponsorshipRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public CreateEventSponsorshipCommandHandler(
        IEventSponsorshipRepository eventSponsorshipRepository,
        IUnitOfWork unitOfWork,
        ResponseMapper responseMapper)
    {
        _eventSponsorshipRepository = eventSponsorshipRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }

    public async Task<EventSponsorshipResponseDTO> Handle(CreateEventSponsorshipCommand command, CancellationToken ct)
    {
        var newEventSponsorship = new EventSponsorship
        {
            ContributionAmount = command.dto.ContributionAmount,
            EventId = command.dto.EventId,
            SponsorId = command.dto.SponsorId
        };

        _eventSponsorshipRepository.Add(newEventSponsorship);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(newEventSponsorship);
    }
}
