using Events.Api.Data;
using Events.Api.Entities;

namespace Events.Api.Repositories;

public class SponsorRepository : Repository<Sponsor>, ISponsorRepository
{
    public SponsorRepository(ApplicationDbContext context) : base(context) {}
}