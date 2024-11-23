using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Core.Interfaces;
using Users.Core.Models;
using Users.Data.DatabaseContext;

namespace Users.Data.Repositories;

public class GenresRepository(UsersDbContext context) : IGenreRepository
{
    public async Task<Genre> AddGenre(Genre genre)
    {
        var createdGenre = await context.Genre.AddAsync(genre);
        await context.SaveChangesAsync();
        return createdGenre.Entity;
    }

    public Task<List<Genre>> GetGenres(Expression<Func<Genre, bool>> predicate)
    {
        return context.Genre
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }
}
