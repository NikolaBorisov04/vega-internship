using Events.Infrastructure.Persistence.Data;
using Events.Domain.Entities;
using Events.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Persistence.Repositories;

public class EventPhotoRepository : Repository<EventPhoto>, IEventPhotoRepository
{
    public EventPhotoRepository(ApplicationDbContext context) : base(context) {}

    public async Task<IReadOnlyList<EventPhoto>> GetByEventIdAsync(
        Guid eventId,
        CancellationToken ct = default)
    {
        return await _dbSet
            .Where(ep => ep.EventId == eventId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}