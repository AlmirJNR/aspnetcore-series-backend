using Data.Models;

namespace Contracts.Categories.Dto;

public class ReducedCategoryDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public static ReducedCategoryDto FromModel(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name
    };
}