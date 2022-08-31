using Data.Models;

namespace Contracts.SeriesContracts.Dto;

public class ReducedSeriesDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public IEnumerable<Category>? Categories { get; set; }

    public static ReducedSeriesDto FromModel(Series seriesModel) => new()
    {
        Id = seriesModel.Id,
        Title = seriesModel.Title,
        Description = seriesModel.Description,
        ImageUrl = seriesModel.ImageUrl,
        Categories = seriesModel.Categories,
    };
}