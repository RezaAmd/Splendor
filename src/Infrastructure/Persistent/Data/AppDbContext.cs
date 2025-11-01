using Domain.Entities.Cards;

namespace Infrastructure.Persistent.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CardEntity> Cards { get; set; } = default!;
    public DbSet<CardCostEntity> CardCosts { get; set; } = default!;
    public DbSet<NobleEntity> NobleTiles { get; set; } = default!;
    public DbSet<NobleCostEntity> NobleTileCosts { get; set; } = default!;
}