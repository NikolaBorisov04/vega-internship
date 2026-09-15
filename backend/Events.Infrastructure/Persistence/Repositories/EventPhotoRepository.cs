using Events.Infrastructure.Persistence.Data;
using Events.Domain.Entities;
using Events.Application.Repositories;

namespace Events.Infrastructure.Persistence.Repositories;

public class EventPhotoRepository : Repository<EventPhoto>, IEventPhotoRepository
{
    public EventPhotoRepository(ApplicationDbContext context) : base(context) {}
}