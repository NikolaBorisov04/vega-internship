using Events.Api.Entities;

namespace Events.Api.Repositories;

public interface IRepository<TEntity> where TEntity : AuditableEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}