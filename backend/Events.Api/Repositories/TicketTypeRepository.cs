using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.Entities;

namespace Events.Api.Repositories;

public class TicketTypeRepository : Repository<TicketType>, ITicketTypeRepository
{
    public TicketTypeRepository(ApplicationDbContext context) : base(context) {}

    public Task<bool> EventExistsAsync(Guid eventId, CancellationToken ct = default)
    {
        return _context.Events.AnyAsync(e => e.Id == eventId, ct);
    }
}