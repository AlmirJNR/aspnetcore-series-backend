using Data.Models;

namespace Contracts.SeriesContracts.Dto;

public class CreateSeriesDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly? ReleaseDate { get; set; }

    public Series ToModel() => new()
    {
        Title = this.Title,
        Description = this.Description,
        ImageUrl = this.ImageUrl,
        ReleaseDate = this.ReleaseDate,
    };
}