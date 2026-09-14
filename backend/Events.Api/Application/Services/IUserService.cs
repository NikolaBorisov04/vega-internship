using Events.Api.DTOs;

namespace Events.Api.Services
{
    public interface IUserService
    {
        Task<UserResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserResponseDTO>> GetAllAsync();
        Task<UserResponseDTO> RegisterCustomerAsync(RegisterCustomerDTO dto, CancellationToken ct = default);
        Task<UserResponseDTO> RegisterOrganizerAsync(RegisterOrganizerDTO dto, CancellationToken ct = default);
        Task<UserResponseDTO> RegisterAdminAsync(RegisterAdminDTO dto, CancellationToken ct = default);
        Task<UserResponseDTO> ValidateUserAsync(string email, string password);
    }
}