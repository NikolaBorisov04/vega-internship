using Events.Application.DTOs;

namespace Events.Application.Services
{
    public interface ISponsorService
    {
        Task<SponsorResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<SponsorResponseDTO>> GetAllAsync();
        Task<SponsorResponseDTO> CreateAsync(SponsorCreateDTO sponsorCreateDto, CancellationToken ct = default);
    }
}