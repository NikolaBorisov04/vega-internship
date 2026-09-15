using Microsoft.EntityFrameworkCore;
using Events.Infrastructure.Persistence.Data;
using Events.Domain.Entities;
using Events.Application.Repositories;

namespace Events.Infrastructure.Persistence.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationDbContext context) : base(context) {}

    public Task<bool> TicketTypeExistsAsync(Guid ticketTypeId, CancellationToken ct = default)
    {
        return _context.TicketTypes
            .AnyAsync(t => t.Id == ticketTypeId, ct);
    }

    public Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken ct = default)
    {
        return _context.Customers
            .AnyAsync(c => c.Id == customerId, ct);
    }
}