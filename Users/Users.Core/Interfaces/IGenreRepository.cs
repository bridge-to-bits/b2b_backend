using System.Linq.Expressions;
using Users.Core.Models;

namespace Users.Core.Interfaces;

public interface IGenreRepository
{
    public Task<List<Genre>> GetGenres(Expression<Func<Genre, bool>> predicate);
}
