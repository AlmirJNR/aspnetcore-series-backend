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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=password;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");

            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.ToTable("series");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.AddedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added_at")
                .HasDefaultValueSql("now()");

            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");

            entity.Property(e => e.Description).HasColumnName("description");

            entity.Property(e => e.ImageUrl).HasColumnName("image_url");

            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");

            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasMany(d => d.Categories)
                .WithMany(p => p.Series)
                .UsingEntity<Dictionary<string, object>>(
                    "SeriesCategory",
                    l => l.HasOne<Category>().WithMany().HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("series_category_category_id_fkey"),
                    r => r.HasOne<Series>().WithMany().HasForeignKey("SeriesId").OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("series_category_series_id_fkey"),
                    j =>
                    {
                        j.HasKey("SeriesId", "CategoryId").HasName("series_category_pkey");

                        j.ToTable("series_category");

                        j.IndexerProperty<Guid>("SeriesId").HasColumnName("series_id");

                        j.IndexerProperty<Guid>("CategoryId").HasColumnName("category_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}