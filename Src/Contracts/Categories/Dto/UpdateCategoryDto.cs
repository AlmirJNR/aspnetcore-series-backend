using Data.Models;

namespace Contracts.Categories.Dto;

public readonly record struct UpdateCategoryDto()
{
    public string? Name { get; init; } = null;

    public Category ToModel() => new()
    {
        Name = Name
    };
}