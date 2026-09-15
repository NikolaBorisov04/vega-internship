using Events.Api.Domain.Entities;

namespace Events.Api.Application.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<bool> TicketTypeExistsAsync(Guid ticketTypeId, CancellationToken ct = default);
    Task<bool> CustomerExistsAsync(Guid customerId, CancellationToken ct = default);
}