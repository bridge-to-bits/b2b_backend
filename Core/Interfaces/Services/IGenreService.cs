using Core.DTOs.Users;
using Core.Models;
using Core.Responses;

namespace Core.Interfaces.Services;

public interface IGenreService
{
    public Task<IEnumerable<GenreResponse>> GetAllGenres();
    public Task<Genre> AddGenre(AddGenreDTO genre);
    public Task RemoveGenre(string genreId);
}
