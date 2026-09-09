using Events.Api.DTOs;

namespace Events.Api.Services
{
    public interface IEventService
    {
        Task<EventResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<EventResponseDTO>> GetAllAsync();
        Task<EventResponseDTO> CreateAsync(EventCreateDTO eventCreateDto, CancellationToken ct = default);
    }
}