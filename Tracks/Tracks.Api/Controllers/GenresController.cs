using Microsoft.AspNetCore.Mvc;
using Tracks.Core.DTOs;
using Tracks.Core.Interfaces;
using Tracks.Core.Models;

namespace Tracks.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var result = await genreService.GetAllGenres();
        return Ok(result);
    }

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
