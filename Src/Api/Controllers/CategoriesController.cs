using Contracts.Categories.Dto;
using Contracts.Categories.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeriesBackend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoriesService;

    public CategoriesController(ICategoryService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    /// <summary>
    /// Creates a new Category
    /// </summary>
    /// <response code="201">Returns the created category</response>
    /// <response code="400">Returns If there was an error</response>
    [HttpPost]
    [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNew([FromBody] CreateCategoryDto categoryDto)
    {
        if (string.IsNullOrWhiteSpace(categoryDto.Name))
        {
            return BadRequest("Missing category name");
        }

        var response = await _categoriesService.AddNew(categoryDto);

        if (response is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(response);
    }

    /// <summary>
    /// Delete multiple Categories
    /// </summary>
    /// <response code="200">Returns Ok</response>
    /// <response code="404">Returns If one or more id where not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAllIds([FromBody] Guid[] ids)
    {
        var response = await _categoriesService.DeleteAllIds(ids);

        if (!response)
        {
            return NotFound("One or more ids where not found");
        }

        return Ok();
    }
    
    /// <summary>
    /// Delete a single Category
    /// </summary>
    /// <response code="200">Returns Ok</response>
    /// <response code="404">Returns If the id was not found</response>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        var response = await _categoriesService.DeleteById(id);

        if (!response)
        {
            return NotFound("Id was not found");
        }

        return Ok();
    }
    
    /// <summary>
    /// Get all Categories, in a paginated way, where include deleted is optionals
    /// </summary>
    /// <response code="200">Returns list of paginated series</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPaginated(
        [FromQuery] bool includeDeleted = false,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 6)
    {
        return Ok(await _categoriesService.GetAllPaginated(includeDeleted: includeDeleted, page: page, limit: limit));
    }
    
    /// <summary>
    /// Get Category details by Id
    /// </summary>
    /// <response code="200">Returns Category</response>
    /// <response code="404">Returns No category with id {id} where found</response>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _categoriesService.GetById(id);

        if (response is null)
        {
            return NotFound("No category with id {id} where found");
        }

        return Ok(response);
    }

    /// <summary>
    /// Update Category by Id
    /// </summary>
    /// <response code="202">Returns updated Category</response>
    /// <response code="404">Returns Id {id} not found</response>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateById([FromRoute] Guid id, UpdateCategoryDto updateCategoryDto)
    {
        var response = await _categoriesService.UpdateById(id, updateCategoryDto);

        if (!response)
        {
            return NotFound($"Id {id} not found");
        }

        return Accepted("Updated with success", response);
    }
}