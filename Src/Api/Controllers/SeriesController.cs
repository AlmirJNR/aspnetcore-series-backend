using Contracts.SeriesContracts.Dto;
using Contracts.SeriesContracts.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace SeriesBackend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SeriesController : ControllerBase
{
    private readonly ISeriesService _seriesService;

    public SeriesController(ISeriesService seriesService)
    {
        _seriesService = seriesService;
    }

    /// <summary>
    /// Creates a new Series
    /// </summary>
    /// <response code="201">Returns the created series</response>
    /// <response code="400">Bad body parameters</response>
    /// <response code="500">If there was an error while inserting a new series to the database</response>
    [HttpPost]
    [ProducesResponseType(typeof(Series), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddNew([FromBody] CreateSeriesDto series)
    {
        if (string.IsNullOrWhiteSpace(series.Title))
        {
            return BadRequest("Title is obligatory");
        }

        var response = await _seriesService.AddNew(series);

        if (response == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Created("", response);
    }

    /// <summary>
    /// Deletes all Series with the correspondent Ids
    /// </summary>
    /// <response code="200">Returns Deleted all series with success</response>
    /// <response code="404">Returns No series with these ids where found</response>
    [HttpDelete]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAllIds([FromBody] Guid[] ids)
    {
        var response = await _seriesService.DeleteAllIds(ids);

        if (!response)
        {
            return NotFound("No series with these ids where found");
        }

        return Ok("Deleted all series with success");
    }

    /// <summary>
    /// Deletes Series by Id
    /// </summary>
    /// <response code="200">Returns Deleted with success</response>
    /// <response code="404">Returns No series with id {id} where found</response>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        var deletedWithSuccess = await _seriesService.DeleteById(id);

        if (!deletedWithSuccess)
        {
            return NotFound($"No series with id {id} where found");
        }

        return Ok("Deleted with success");
    }

    /// <summary>
    /// Get all Series, in a paginated way, where include deleted is optionals
    /// </summary>
    /// <response code="200">Returns list of paginated series</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Series>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPaginated(
        [FromQuery] bool includeDeleted = false,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 6)
    {
        return Ok(await _seriesService.GetAllPaginated(includeDeleted, page, limit));
    }

    /// <summary>
    /// Get Series details by Id
    /// </summary>
    /// <response code="200">Returns Series</response>
    /// <response code="404">Returns No series with id {id} where found</response>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(Series), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _seriesService.GetById(id);

        if (response == null)
        {
            return NotFound($"No series with id {id} where found");
        }

        return Ok(response);
    }

    public record CategoryIdContainer(Guid CategoryId);

    /// <summary>
    /// Insert new category to existing series
    /// </summary>
    /// <response code="200">Returns Series Category was updated</response>
    /// <response code="400">Returns when request data is invalid</response>
    /// <response code="409">Returns when </response>
    [HttpPost("{id:Guid}/categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> InsertNewCategory([FromRoute] Guid id, [FromBody] CategoryIdContainer body)
    {
        try
        {
            var response = await _seriesService.InsertNewCategory(id, body.CategoryId);

            if (!response)
            {
                return BadRequest();
            }

            return Ok("Series Category was updated");
        }
        catch (Exception error)
        {
            return Conflict();
        }
    }

    /// <summary>
    /// Update Series by Id
    /// </summary>
    /// <response code="200">Returns Series was updated</response>
    /// <response code="404">Returns No series with id {id} where found</response>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateSeriesDto updateSeriesDto)
    {
        var response = await _seriesService.UpdateById(id, updateSeriesDto);

        if (!response)
        {
            return NotFound($"No series with id {id} where found");
        }

        return Ok("Series was updated");
    }
}