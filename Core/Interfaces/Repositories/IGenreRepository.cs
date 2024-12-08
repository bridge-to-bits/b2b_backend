using Core.Models;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories;

public interface IGenreRepository
{
    public Task<Genre> AddGenre(Genre genre);
    public Task<List<Genre>> GetGenres(Expression<Func<Genre, bool>> predicate);
    public Task<List<Genre>> GetAllGenres();
    public Task<Genre?> GetGenre(Expression<Func<Genre, bool>> predicate);
    public Task RemoveGenre(Genre genre);
}
