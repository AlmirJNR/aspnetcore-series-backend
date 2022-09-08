using Data.Models;

namespace Contracts.Categories.Dto;

public readonly record struct ReducedCategoryDto()
{
    public Guid Id { get; init; } = Guid.Empty;
    public string? Name { get; init; } = null;

    public static ReducedCategoryDto FromModel(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name
    };
}