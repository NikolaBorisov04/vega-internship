using Events.Application.Repositories;
using Events.Domain.Entities;
using Events.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Persistence.Repositories;

public class OrganizerRepository : Repository<Organizer>, IOrganizerRepository
{
    public OrganizerRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    public async Task<Organizer?> GetByEventIdAsync(
        Guid eventId,
        CancellationToken ct = default)
    {
        return await _dbSet
            .Where(o => o.OrganizedEvents.Any(e => e.Id == eventId))
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);
    }
}