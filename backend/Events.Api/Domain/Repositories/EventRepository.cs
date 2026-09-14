using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.Entities;

namespace Events.Api.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context): base(context) {}

    public Task<bool> OrganizerExistsAsync(Guid organizerId, CancellationToken ct = default)
    {
        return _context.Organizers
            .AnyAsync(o => o.Id == organizerId, ct);
    }
}