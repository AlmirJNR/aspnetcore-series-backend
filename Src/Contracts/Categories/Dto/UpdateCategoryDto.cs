using Data.Models;

namespace Contracts.Categories.Dto;

public class UpdateCategoryDto
{
    public string? Name { get; set; }

    public Category ToModel() => new Category()
    {
        Name = this.Name
    };
}