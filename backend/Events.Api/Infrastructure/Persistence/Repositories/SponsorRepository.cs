using Events.Api.Infrastructure.Persistence.Data;
using Events.Api.Domain.Entities;
using Events.Api.Application.Repositories;

namespace Events.Api.Infrastructure.Persistence.Repositories;

public class SponsorRepository : Repository<Sponsor>, ISponsorRepository
{
    public SponsorRepository(ApplicationDbContext context) : base(context) {}
}