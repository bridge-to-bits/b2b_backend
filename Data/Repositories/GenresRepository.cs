using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Models;
using Core.Interfaces.Repositories;

namespace Data.Repositories;

public class GenresRepository(B2BDbContext context) : IGenreRepository
{
    public async Task<Genre> AddGenre(Genre genre)
    {
        var createdGenre = await context.Genres.AddAsync(genre);
        await context.SaveChangesAsync();
        return createdGenre.Entity;
    }

    public Task<List<Genre>> GetGenres(Expression<Func<Genre, bool>> predicate)
    {
        return context.Genres
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public Task RemoveGenre(Genre genre)
    {
        context.Genres.Remove(genre);
        return context.SaveChangesAsync();
    }

    public Task<List<Genre>> GetAllGenres()
    {
        return context.Genres.AsNoTracking().ToListAsync();
    }

    public Task<Genre?> GetGenre(Expression<Func<Genre, bool>> predicate)
    {
        return context.Genres.AsNoTracking().FirstOrDefaultAsync(predicate);
    }
}
