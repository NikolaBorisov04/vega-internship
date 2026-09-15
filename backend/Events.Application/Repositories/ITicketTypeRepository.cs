using Events.Domain.Entities;

namespace Events.Application.Repositories;

public interface ITicketTypeRepository : IRepository<TicketType>
{
    Task<bool> EventExistsAsync(Guid EventId, CancellationToken ct = default);
    Task<Guid?> GetEventOrganizerIdAsync(Guid eventId, CancellationToken ct = default);
    Task<IReadOnlyList<TicketType>> GetByEventIdAsync(Guid eventId, CancellationToken ct = default);
}