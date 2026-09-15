using Events.Api.Domain.Entities;

namespace Events.Api.Application.Repositories;

public interface IEventRepository : IRepository<Event>
{
    Task<bool> OrganizerExistsAsync(Guid organizerId, CancellationToken ct = default);
}