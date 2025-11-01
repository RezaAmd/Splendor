using Domain.Common;

namespace Application.Common.Interfaces;

public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> FindByIdAsync(Guid id, CancellationToken ct = default);

    void Create(TEntity entity);
    Task CreateAsync(TEntity entity, CancellationToken ct);
    void Create(List<TEntity> entities);
    Task CreateAsync(List<TEntity> entities, CancellationToken ct);

    void Update(TEntity entity);
    void Update(List<TEntity> entities);

    void Remove(TEntity entity);
    void Remove(List<TEntity> entities);
}