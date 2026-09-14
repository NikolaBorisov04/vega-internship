using Microsoft.EntityFrameworkCore;
using Events.Api.Data;
using Events.Api.Entities;

namespace Events.Api.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : AuditableEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync([id], ct);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet
            .AnyAsync(x => x.Id == id, ct);
    }

    public virtual bool Add(TEntity entity)
    {
        var entry = _dbSet.Add(entity);
        return entry.State == EntityState.Added;
    }

    public virtual bool Update(TEntity entity)
    {
        var entry = _dbSet.Update(entity);
        return entry.State == EntityState.Modified;
    }

    public virtual bool Delete(TEntity entity)
    {
        var entry = _dbSet.Remove(entity);
        return entry.State == EntityState.Deleted;
    }
}