using Microsoft.EntityFrameworkCore;
using Events.Infrastructure.Persistence.Data;
using Events.Domain.Entities;
using Events.Application.Repositories;

namespace Events.Infrastructure.Persistence.Repositories;

public class TicketTypeRepository : Repository<TicketType>, ITicketTypeRepository
{
    public TicketTypeRepository(ApplicationDbContext context) : base(context) {}

    public Task<bool> EventExistsAsync(Guid eventId, CancellationToken ct = default)
    {
        return _context.Events.AnyAsync(e => e.Id == eventId, ct);
    }

    public async Task<IReadOnlyList<TicketType>> GetByEventIdAsync(Guid eventId, CancellationToken ct = default)
    {
        return await _dbSet
            .Where(tt => tt.EventId == eventId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}