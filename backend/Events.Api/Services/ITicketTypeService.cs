using Events.Api.DTOs;

namespace Events.Api.Services;

public interface ITicketTypeService
{
    Task<TicketTypeResponseDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<TicketTypeResponseDTO>> GetAllAsync();
    Task<TicketTypeResponseDTO> CreateAsync(TicketTypeCreateDTO ticketCreateDTO, CancellationToken ct);
}