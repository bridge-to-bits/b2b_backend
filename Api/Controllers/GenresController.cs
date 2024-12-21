using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Core.DTOs.Users;
using Core.Interfaces.Services;
using Data.Repositories;
using Core.Responses;

namespace Api.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
    [ProducesResponseType(typeof(IEnumerable<GenreResponse>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var result = await genreService.GetAllGenres();
        return Ok(result);
    }

    [ProducesResponseType(typeof(IEnumerable<Genre>), StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre(AddGenreDTO addGenreDTO)
    {
        var result = await genreService.AddGenre(addGenreDTO);
        return Ok(result);
    }

    [HttpDelete("{genreId}")]
    public async Task<IActionResult> DeleteGenre(string genreId)
    {
        await genreService.RemoveGenre(genreId);
        return Ok();
    }
}
