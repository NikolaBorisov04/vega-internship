using Events.Api.Entities;

namespace Events.Api.Repositories;

public interface ITicketTypeRepository : IRepository<TicketType>
{
    Task<bool> EventExistsAsync(Guid EventId, CancellationToken ct = default);
}