using Events.Application.DTOs;

namespace Events.Application.Services
{
    public interface IEventSponsorshipService
    {
        Task<EventSponsorshipResponseDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<EventSponsorshipResponseDTO>> GetAllAsync();
        Task<EventSponsorshipResponseDTO> CreateAsync(EventSponsorshipCreateDTO eventSponsorshipCreateDto, CancellationToken ct = default);
    }
}