using Events.Api.Entities;

namespace Events.Api.IRepositories;

public interface IEventRepository : IRepository<Event>
{
    Task<bool> OrganizerExistsAsync(Guid organizerId, CancellationToken ct = default);
}