using Events.Api.Application.Repositories;
using Events.Api.Application.Mappers;
using Events.Api.Application.DTOs;
using Events.Api.Domain.Entities;

namespace Events.Api.Application.Services;

public class TicketTypeService : ITicketTypeService
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;
    private readonly ICurrentUserService _currentUserService;

    public TicketTypeService(ITicketTypeRepository ticketTypeRepository, IUnitOfWork unitOfWork, ResponseMapper responseMapper, ICurrentUserService currentUserService)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
        _currentUserService = currentUserService;
    }

    public async Task<TicketTypeResponseDTO> GetByIdAsync(Guid id)
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

    public async Task<IEnumerable<TicketTypeResponseDTO>> GetByEventIdAsync(Guid eventId, CancellationToken ct = default)
    {

        var eventExists = await _ticketTypeRepository.EventExistsAsync(eventId, ct);

        if (!eventExists)
        {
            throw new KeyNotFoundException($"Dogadjaj sa ID-jem '{eventId}' ne postoji.");
        }

        var ticketTypes = await _ticketTypeRepository.GetByEventIdAsync(eventId, ct);

        return ticketTypes
            .Select(_responseMapper.MapToResponse)
            .ToList();
    }

    public async Task<TicketTypeResponseDTO> CreateAsync(TicketTypeCreateDTO dto, CancellationToken ct = default)
    {
        var eventOrganizerId =
            await _ticketTypeRepository.GetEventOrganizerIdAsync(dto.EventId, ct);

        if (eventOrganizerId == null)
        {
            throw new KeyNotFoundException(
                $"Dogadjaj sa ID-jem '{dto.EventId}' ne postoji.");
        }

        if (!_currentUserService.IsAdmin && eventOrganizerId != _currentUserService.UserId)
        {
            throw new UnauthorizedAccessException(
                "Nemate dozvolu da kreirate tip tiketa za ovaj dogadjaj.");
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