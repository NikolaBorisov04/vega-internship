using Events.Api.Application.DTOs;

namespace Events.Api.Application.Services
{
    public interface IEventService
    {
        Task<EventResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<EventResponseDTO>> GetAllAsync();
        Task<EventResponseDTO> CreateAsync(EventCreateDTO eventCreateDto, Guid organizerId, CancellationToken ct = default);
    }
}