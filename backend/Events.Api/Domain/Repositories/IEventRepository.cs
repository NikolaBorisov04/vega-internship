using Events.Api.Entities;

namespace Events.Api.Repositories;

public interface IEventRepository : IRepository<Event>
{
    Task<bool> OrganizerExistsAsync(Guid organizerId, CancellationToken ct = default);
}