using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.Entities;
using Events.Api.IRepositories;

namespace Events.Api.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) {}

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, ct);
    }

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
    {
        return _dbSet
            .AnyAsync(u => u.Email == email, ct);
    }
}