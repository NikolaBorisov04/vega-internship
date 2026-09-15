using Events.Domain.Entities;

namespace Events.Application.Repositories;

public interface IRepository<TEntity> where TEntity : AuditableEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);

    bool Add(TEntity entity);

    bool Update(TEntity entity);

    bool Delete(TEntity entity);
}