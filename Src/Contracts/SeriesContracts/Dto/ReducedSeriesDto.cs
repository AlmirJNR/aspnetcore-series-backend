using Data.Models;

namespace Contracts.SeriesContracts.Dto;

public readonly record struct  ReducedSeriesDto()
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Title { get; init; } = null!;
    public string? Description { get; init; } = null;
    public string? ImageUrl { get; init; } = null;
    public IEnumerable<Category>? Categories { get; init; } = null;

    public static ReducedSeriesDto FromModel(Series seriesModel) => new()
    {
        Id = seriesModel.Id,
        Title = seriesModel.Title,
        Description = seriesModel.Description,
        ImageUrl = seriesModel.ImageUrl,
        Categories = seriesModel.Categories,
    };
}