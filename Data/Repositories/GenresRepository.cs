using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models;

namespace Data.Repositories;

public class GenresRepository(B2BDbContext context) : IGenreRepository
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
