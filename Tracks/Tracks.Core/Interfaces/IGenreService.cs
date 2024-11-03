using Tracks.Core.DTOs;
using Tracks.Core.Models;
using Tracks.Core.Responses;

namespace Tracks.Core.Interfaces
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreResponse>> GetAllGenres();
        public Task<Genre> AddGenre(AddGenreDTO genre);
        public Task RemoveGenre(string genreId);
    }
}
