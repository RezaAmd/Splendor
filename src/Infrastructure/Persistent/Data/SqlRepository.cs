using Application.Common.Interfaces;
using Domain.Common;

namespace Infrastructure.Persistent.Data;

public class SqlRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public SqlRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public void Create(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    public void Create(List<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public async Task CreateAsync(List<TEntity> entities, CancellationToken ct = default)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void Remove(List<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Update(List<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }
}