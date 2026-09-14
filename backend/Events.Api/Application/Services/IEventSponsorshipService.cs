using Events.Api.DTOs;

namespace Events.Api.Services
{
    public interface IEventSponsorshipService
    {
        Task<EventSponsorshipResponseDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<EventSponsorshipResponseDTO>> GetAllAsync();
        Task<EventSponsorshipResponseDTO> CreateAsync(EventSponsorshipCreateDTO eventSponsorshipCreateDto, CancellationToken ct = default);
    }
}