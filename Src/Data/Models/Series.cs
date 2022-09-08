namespace Data.Models;

public partial class Series
{
    public Series()
    {
        Categories = new HashSet<Category>();
    }

    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? AddedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Category> Categories { get; set; }
}