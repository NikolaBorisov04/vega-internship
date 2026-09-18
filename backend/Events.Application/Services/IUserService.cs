using Events.Application.DTOs;

namespace Events.Application.Services
{
    public interface IUserService
    {
        Task<UserResponseDTO> ValidateUserAsync(string email, string password);
    }
}