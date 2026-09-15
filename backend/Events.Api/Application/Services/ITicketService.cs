using Events.Api.Application.DTOs;

namespace Events.Api.Application.Services
{
    public interface ITicketService
    {
        Task<TicketResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<TicketResponseDTO>> GetAllAsync();
        Task<TicketResponseDTO> CreateAsync(TicketCreateDTO ticketCreateDto, Guid customerId, CancellationToken ct = default);
    }
}