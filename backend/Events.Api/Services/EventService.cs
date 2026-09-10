using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.DTOs;
using Events.Api.Entities;
using Events.Api.Extensions;
using Events.Api.Mappings;

namespace Events.Api.Services;
public class EventService : IEventService
{
    private readonly ApplicationDbContext _context;
    private readonly ResponseMapper _responseMapper;

    public EventService(ApplicationDbContext context, ResponseMapper responseMapper)
    {
        _context = context;
        _responseMapper = responseMapper;
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

        return _responseMapper.MapToResponse(_event);
    }
}