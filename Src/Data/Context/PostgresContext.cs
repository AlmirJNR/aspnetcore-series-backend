using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Series> Series { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresContext).Assembly);
    }
}