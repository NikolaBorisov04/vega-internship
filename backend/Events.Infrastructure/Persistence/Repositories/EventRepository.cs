using Microsoft.EntityFrameworkCore;
using Events.Infrastructure.Persistence.Data;
using Events.Domain.Entities;
using Events.Application.Repositories;

namespace Events.Infrastructure.Persistence.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context): base(context) {}

    public Task<bool> OrganizerExistsAsync(Guid organizerId, CancellationToken ct = default)
    {
        return _context.Organizers
            .AnyAsync(o => o.Id == organizerId, ct);
    }
}