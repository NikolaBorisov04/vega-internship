using Events.Api.DTOs;

namespace Events.Api.Services
{
    public interface ISponsorService
    {
        Task<SponsorResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<SponsorResponseDTO>> GetAllAsync();
        Task<SponsorResponseDTO> CreateAsync(SponsorCreateDTO sponsorCreateDto, CancellationToken ct = default);
    }
}