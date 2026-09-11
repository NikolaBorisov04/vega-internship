using Microsoft.EntityFrameworkCore;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Extensions;
using Events.Api.Mappings;
using Events.Api.Repositories;

namespace Events.Api.Services;
public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResponseMapper _responseMapper;

    public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork, ResponseMapper responseMapper)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _responseMapper = responseMapper;
    }
    public async Task<EventResponseDTO?> GetByIdAsync(Guid id)
    {
        var _event = await _eventRepository.GetByIdAsync(id);

        if (_event == null)
        {
            return null;
        }

        return _responseMapper.MapToResponse(_event);
    }
    public async Task<IEnumerable<EventResponseDTO>> GetAllAsync()
    {
        var _events = await _eventRepository.GetAllAsync();
        return _events.Select(_responseMapper.MapToResponse).ToList();
    }
    public async Task<EventResponseDTO> CreateAsync(EventCreateDTO dto, CancellationToken ct = default)
    {
        var organizerExists = await _eventRepository.OrganizerExistsAsync(dto.OrganizerId, ct);

        if (!organizerExists)
        {
            throw new KeyNotFoundException(
                $"Organizer sa ID-jem '{dto.OrganizerId}' ne postoji.");
        }

        var _event = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            MainImageURL = dto.MainImageURL,
            VenueName = dto.VenueName,
            StartOfEvent = dto.StartOfEvent,
            EndOfEvent = dto.EndOfEvent,
            OrganizerId = dto.OrganizerId
        };

        _eventRepository.Add(_event);
        await _unitOfWork.SaveChangesAsync(ct);
        return _responseMapper.MapToResponse(_event);
    }
}