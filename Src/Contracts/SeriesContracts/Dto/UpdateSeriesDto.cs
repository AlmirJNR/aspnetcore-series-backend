using Data.Models;

namespace Contracts.SeriesContracts.Dto;

public readonly record struct UpdateSeriesDto()
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; } = null;
    public string? ImageUrl { get; init; } = null;
    public DateTime? ReleaseDate { get; init; } = null;

    public Series ToModel() => new()
    {
        Title = Title,
        Description = Description,
        ImageUrl = ImageUrl,
        ReleaseDate = ReleaseDate,
    };
}