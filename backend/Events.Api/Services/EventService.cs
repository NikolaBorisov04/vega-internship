using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Extensions;

namespace Events.Api.Services;
public class EventService : IEventService
{
    private readonly ApplicationDbContext _context;

    public EventService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<EventResponseDTO?> GetByIdAsync(Guid id)
    {
        return await _context.Events
                .Where(e => e.Id == id)
                //extension here
                .ToEventResponseDTO()
                .FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<EventResponseDTO>> GetAllAsync()
    {
        return await _context.Events
            .ToEventResponseDTO()
            .ToListAsync();
    }
    public async Task<EventResponseDTO> CreateAsync(EventCreateDTO dto, CancellationToken ct = default)
    {
        var organizerExists = await _context.Organizers
            .AnyAsync(o => o.Id == dto.OrganizerId, ct);

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
            DateAndTimeOfEvent = dto.DateAndTimeOfEvent,
            OrganizerId = dto.OrganizerId
        };

        _context.Events.Add(_event);
        await _context.SaveChangesAsync(ct);

        return MapToResponse(_event);
    }
    private static EventResponseDTO MapToResponse(Event _event)
    {
        return new EventResponseDTO(
            _event.Id,
            _event.Title,
            _event.Description,
            _event.Country,
            _event.City,
            _event.Address,
            _event.MainImageURL,
            _event.VenueName,
            _event.DateAndTimeOfEvent,
            _event.OrganizerId,
            _event.EventPhotosURL
        );
    }
}