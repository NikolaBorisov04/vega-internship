using Microsoft.EntityFrameworkCore;
using Events.Api.Infrastructure.Persistence.Data;
using Events.Api.Domain.Entities;
using Events.Api.Application.Repositories;

namespace Events.Api.Infrastructure.Persistence.Repositories;

public class EventSponsorshipRepository : Repository<EventSponsorship>, IEventSponsorshipRepository
{
    public EventSponsorshipRepository(ApplicationDbContext context): base(context) {}

    public Task<bool> EventExistsAsync(Guid eventId, CancellationToken ct = default)
    {
        return _context.Events
            .AnyAsync(e => e.Id == eventId, ct);
    }

    public Task<bool> SponsorExistsAsync(Guid sponsorId, CancellationToken ct = default)
    {
        return _context.Sponsors
            .AnyAsync(s => s.Id == sponsorId, ct);
    }
}