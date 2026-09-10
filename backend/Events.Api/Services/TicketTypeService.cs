using Events.Api.Repositories;
using Events.Api.Mappings;
using Events.Api.DTOs;
using Events.Api.Entities;

namespace Events.Api.Services;

public class TicketTypeServices : ITicketTypeService
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public async Task<TicketTypeResponseDTO?> GetByIdAsync(Guid id)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(id);

        if(ticketType == null)
        {
            return null;
        }

        return _responseMapper.MapToResponse(ticketType);
    }

    public async Task<IEnumerable<TicketTypeResponseDTO>> GetAllAsync()
    {
        var ticketTypes = await _ticketTypeRepository.GetAllAsync();

        return ticketTypes.Select(_responseMapper.MapToResponse).ToList();
    }

    public async Task<TicketTypeResponseDTO> CreateAsync(TicketTypeCreateDTO dto, CancellationToken ct = default)
    {

        var eventExists = await _ticketTypeRepository.EventExistsAsync(dto.EventId, ct);

        if (!eventExists)
        {
            throw new KeyNotFoundException($"Dogadjaj sa ID-jem '{dto.EventId}' ne postoji.");
        }

        var ticketType = new TicketType
        {
            Name = dto.Name,
            Price = dto.Price,
            EventId = dto.EventId,
            Description = dto.Description,
            QuantityAvailable = dto.QuantityAvailable,
            TicketBackgroundImageUrl = dto.TicketBackgroundImageUrl
        };

        _ticketTypeRepository.Add(ticketType);

        await _unitOfWork.SaveChangesAsync(ct);

        return _responseMapper.MapToResponse(ticketType);
    }
}