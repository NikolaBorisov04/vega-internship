using Microsoft.EntityFrameworkCore;
using Events.Api.Infrastructure.Persistence.Data;
using Events.Api.Domain.Entities;
using Events.Api.Application.Repositories;

namespace Events.Api.Infrastructure.Persistence.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context): base(context) {}

    public Task<bool> OrganizerExistsAsync(Guid organizerId, CancellationToken ct = default)
    {
        return _context.Organizers
            .AnyAsync(o => o.Id == organizerId, ct);
    }
}