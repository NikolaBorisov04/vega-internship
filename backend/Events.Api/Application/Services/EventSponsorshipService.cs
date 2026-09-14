using Microsoft.EntityFrameworkCore;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Mappings;
using Events.Api.IRepositories;

namespace Events.Api.Services;
public class EventSponsorshipService : IEventSponsorshipService
{
    private readonly IEventSponsorshipRepository _eventSponsorshipRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public EventSponsorshipService(IEventSponsorshipRepository eventSponsorshipRepository, IUnitOfWork unitOfWork, ResponseMapper responseMapper)
    {
        _eventSponsorshipRepository = eventSponsorshipRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }
    public async Task<EventSponsorshipResponseDTO?> GetByIdAsync(Guid id)
    {
        var eventSponsorship = await _eventSponsorshipRepository.GetByIdAsync(id);

        if (eventSponsorship == null)
        {
            return null;
        }

        return _responseMapper.MapToResponse(eventSponsorship);
    }
    public async Task<IEnumerable<EventSponsorshipResponseDTO>> GetAllAsync()
    {
        var eventSponsorships = await _eventSponsorshipRepository.GetAllAsync();
        return eventSponsorships.Select(_responseMapper.MapToResponse).ToList();
    }
    public async Task<EventSponsorshipResponseDTO> CreateAsync(EventSponsorshipCreateDTO dto, CancellationToken ct = default)
    {
        var eventExists = await _eventSponsorshipRepository.EventExistsAsync(dto.EventId, ct);

        if (!eventExists)
        {
            throw new KeyNotFoundException($"Dogadjaj sa ID-jem '{dto.EventId}' ne postoji.");
        }
        var sponsorExists = await _eventSponsorshipRepository.SponsorExistsAsync(dto.SponsorId, ct);
        if(!sponsorExists)
        {
            throw new KeyNotFoundException($"Sponzor sa ID-jem {dto.SponsorId} ne postoji.");
        }

        var eventSponsorship = new EventSponsorship
        {
            ContributionAmount = dto.ContributionAmount,
            EventId = dto.EventId,
            SponsorId = dto.SponsorId
        };

        _eventSponsorshipRepository.Add(eventSponsorship);
        await _unitOfWork.SaveChangesAsync(ct);
        return _responseMapper.MapToResponse(eventSponsorship);
    }
}