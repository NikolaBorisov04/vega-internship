using Events.Domain.Entities;

namespace Events.Application.Repositories;

public interface IOrganizerRepository : IRepository<Organizer>
{
    Task<Organizer?> GetByEventIdAsync(Guid eventId, CancellationToken ct = default);
}