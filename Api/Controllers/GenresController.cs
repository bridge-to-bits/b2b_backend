using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Models;
using Core.DTOs.Users;

namespace Api.Controllers;

[Route("api/genres")]
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
