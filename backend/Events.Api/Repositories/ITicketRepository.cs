using Events.Api.Entities;

namespace Events.Api.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<bool> TicketTypeExistsAsync(Guid ticketTypeId, CancellationToken ct = default);
    Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken ct = default);
}