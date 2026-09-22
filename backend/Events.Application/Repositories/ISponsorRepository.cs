using Events.Domain.Entities;

namespace Events.Application.Repositories;

public interface ISponsorRepository : IRepository<Sponsor>
{
    Task<IReadOnlyList<Sponsor>> GetByEventIdAsync(Guid eventId, CancellationToken ct = default);
}