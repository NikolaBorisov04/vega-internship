using Events.Api.DTOs;

namespace Events.Api.Services
{
    public interface ITicketService
    {
        Task<TicketResponseDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<TicketResponseDTO>> GetAllAsync();
        Task<TicketResponseDTO> CreateAsync(TicketCreateDTO ticketCreateDto, Guid customerId, CancellationToken ct = default);
    }
}