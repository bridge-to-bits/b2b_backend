using System.Linq.Expressions;
using Tracks.Core.Models;

namespace Tracks.Core.Interfaces
{
    public interface IGenreRepository
    {
        public Task<List<Genre>> GetAllGenres();
        public Task<Genre?> GetGenre(Expression<Func<Genre, bool>> predicate);
        public Task<Genre> AddGenre(Genre addGenreDTO);
        public Task RemoveGenre(Genre genre);
    }
}
