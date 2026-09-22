using Events.Infrastructure.Persistence.Data;
using Events.Domain.Entities;
using Events.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Persistence.Repositories;

public class SponsorRepository : Repository<Sponsor>, ISponsorRepository
{
    public SponsorRepository(ApplicationDbContext context) : base(context) {}

    public async Task<IReadOnlyList<Sponsor>> GetByEventIdAsync(
        Guid eventId,
        CancellationToken ct = default)
    {
        return await _dbSet
            .Where(s => s.SponsoredEvents.Any(es => es.EventId == eventId))
            .AsNoTracking()
            .ToListAsync(ct);
    }
}