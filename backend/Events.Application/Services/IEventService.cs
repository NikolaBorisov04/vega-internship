using Events.Application.DTOs;

namespace Events.Application.Services
{
    public interface IEventService
    {
        Task<EventResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<EventResponseDTO>> GetAllAsync();
        Task<EventResponseDTO> CreateAsync(EventCreateDTO eventCreateDto, Guid organizerId, CancellationToken ct = default);
    }
}