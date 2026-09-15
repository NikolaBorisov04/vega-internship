using Events.Domain.Entities;

namespace Events.Application.Repositories;

public interface IEventSponsorshipRepository : IRepository<EventSponsorship>
{
    Task<bool> EventExistsAsync(Guid eventId, CancellationToken ct = default);
    Task<bool> SponsorExistsAsync(Guid sponsorId, CancellationToken ct = default);
}