using Events.Domain.Entities;

namespace Events.Application.Repositories;

public interface IEventPhotoRepository : IRepository<EventPhoto>
{
    Task<IReadOnlyList<EventPhoto>> GetByEventIdAsync(Guid eventId, CancellationToken ct = default);
}