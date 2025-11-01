using Domain.Entities.Cards;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<CardEntity> Cards { get; }
    DbSet<CardCostEntity> CardCosts { get; }
    DbSet<NobleEntity> NobleTiles { get; }
    DbSet<NobleCostEntity> NobleTileCosts { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<Result> SaveChangeAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync();
}