using Events.Domain.Entities;

namespace Events.Application.Repositories;

public interface IEventRepository : IRepository<Event>
{
    Task<bool> OrganizerExistsAsync(Guid organizerId, CancellationToken ct = default);
    Task<Guid> GetEventOrganizerIdAsync(Guid eventId, CancellationToken ct = default);

}