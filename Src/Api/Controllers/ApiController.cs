#pragma warning disable

using Microsoft.AspNetCore.Mvc;

namespace SeriesBackend.Controllers;

[ApiController]
[Route("[controller]/v1")]
public class ApiController : ControllerBase
{
    private const string Creator = "Almir Junior";

    /// <summary>
    /// Default Api check-status response
    /// </summary>
    /// <response code="200">Returns the default response</response>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get() => Ok($"Welcome to series api, made by: {Creator}");
}
