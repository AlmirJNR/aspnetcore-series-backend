using Data.Models;

namespace Contracts.Categories.Dto;

public class CreateCategoryDto
{
    public string? Name { get; set; }

    public Category ToModel() => new Category()
    {
        Name = this.Name
    };
}