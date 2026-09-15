using Events.Api.Application.DTOs;

namespace Events.Api.Application.Services
{
    public interface IEventSponsorshipService
    {
        Task<EventSponsorshipResponseDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<EventSponsorshipResponseDTO>> GetAllAsync();
        Task<EventSponsorshipResponseDTO> CreateAsync(EventSponsorshipCreateDTO eventSponsorshipCreateDto, CancellationToken ct = default);
    }
}