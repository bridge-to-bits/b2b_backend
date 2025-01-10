using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Core.DTOs.Users;
using Core.Interfaces.Services;
using Core.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
    [SwaggerOperation(Summary = "Get all genres")]
    [ProducesResponseType(typeof(IEnumerable<GenreResponse>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var result = await genreService.GetAllGenres();
        return Ok(result);
    }

    [SwaggerOperation(Summary = "Add new genre")]
    [ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre(AddGenreDTO addGenreDTO)
    {
        var result = await genreService.AddGenre(addGenreDTO);
        return Ok(result);
    }

    [SwaggerOperation(Summary = "Delete specific genre")]
    [HttpDelete("{genreId}")]
    public async Task<IActionResult> DeleteGenre(string genreId)
    {
        await genreService.RemoveGenre(genreId);
        return Ok();
    }
}
