using Events.Application.DTOs;

namespace Events.Application.Services
{
    public interface ITicketService
    {
        Task<TicketResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<TicketResponseDTO>> GetAllAsync();
        Task<TicketResponseDTO> CreateAsync(TicketCreateDTO ticketCreateDto, Guid customerId, CancellationToken ct = default);
    }
}