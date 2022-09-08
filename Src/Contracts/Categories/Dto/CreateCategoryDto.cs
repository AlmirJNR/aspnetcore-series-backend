using Data.Models;

namespace Contracts.Categories.Dto;

public readonly record struct CreateCategoryDto()
{
    public string? Name { get; init; } = null;

    public Category ToModel() => new()
    {
        Name = Name
    };
}