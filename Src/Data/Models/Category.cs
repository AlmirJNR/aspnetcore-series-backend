namespace Data.Models;

public partial class Category
{
    public Category()
    {
        Series = new HashSet<Series>();
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<Series> Series { get; set; }
}