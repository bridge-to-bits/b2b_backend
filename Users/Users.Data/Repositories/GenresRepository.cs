using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Core.Interfaces;
using Users.Core.Models;
using Users.Data.DatabaseContext;

namespace Users.Data.Repositories;

public class GenresRepository(UsersDbContext context) : IGenreRepository
{
    public Task<List<Genre>> GetGenres(Expression<Func<Genre, bool>> predicate)
    {
        return context.Genres
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }
}
