using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.Entities;
using Events.Api.IRepositories;

namespace Events.Api.Repositories;

public class TicketTypeRepository : Repository<TicketType>, ITicketTypeRepository
{
    public TicketTypeRepository(ApplicationDbContext context) : base(context) {}

    public Task<bool> EventExistsAsync(Guid eventId, CancellationToken ct = default)
    {
        return _context.Events.AnyAsync(e => e.Id == eventId, ct);
    }

    public async Task<Guid?> GetEventOrganizerIdAsync(Guid eventId, CancellationToken ct = default)
    {
        return await _context.Events
            .Where(e => e.Id == eventId)
            .Select(e => e.OrganizerId)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IReadOnlyList<TicketType>> GetByEventIdAsync(Guid eventId, CancellationToken ct = default)
    {
        return await _dbSet
            .Where(tt => tt.EventId == eventId)
            .AsNoTracking()
            .ToListAsync(ct);
    }
}