using Events.Api.Application.DTOs;

namespace Events.Api.Application.Services
{
    public interface ISponsorService
    {
        Task<SponsorResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<SponsorResponseDTO>> GetAllAsync();
        Task<SponsorResponseDTO> CreateAsync(SponsorCreateDTO sponsorCreateDto, CancellationToken ct = default);
    }
}