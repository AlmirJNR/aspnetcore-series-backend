using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration;

public class SeriesEntityConfiguration: IEntityTypeConfiguration<Series>
{
    public void Configure(EntityTypeBuilder<Series> builder)
    {
        builder.ToTable("series");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(e => e.AddedAt)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("added_at")
            .HasDefaultValueSql("now()");

        builder.Property(e => e.DeletedAt)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("deleted_at");

        builder.Property(e => e.Description).HasColumnName("description");

        builder.Property(e => e.ImageUrl).HasColumnName("image_url");

        builder.Property(e => e.ReleaseDate).HasColumnName("release_date");

        builder.Property(e => e.Title).HasColumnName("title");

        builder.HasMany(d => d.Categories)
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
    }
}