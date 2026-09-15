using Events.Infrastructure.Persistence.Data;
using Events.Domain.Entities;
using Events.Application.Repositories;

namespace Events.Infrastructure.Persistence.Repositories;

public class SponsorRepository : Repository<Sponsor>, ISponsorRepository
{
    public SponsorRepository(ApplicationDbContext context) : base(context) {}
}