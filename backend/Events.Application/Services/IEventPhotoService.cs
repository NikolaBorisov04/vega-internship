using Events.Application.DTOs;

namespace Events.Application.Services
{
    public interface IEventPhotoService
    {
        Task<EventPhotoResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<EventPhotoResponseDTO>> GetAllAsync();
        Task<EventPhotoResponseDTO> CreateAsync(EventPhotoCreateDTO eventphotoCreateDto, CancellationToken ct = default);
    }
}