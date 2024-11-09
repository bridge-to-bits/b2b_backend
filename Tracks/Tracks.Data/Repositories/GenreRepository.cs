using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tracks.Core.Interfaces;
using Tracks.Core.Models;
using Tracks.Data.DatabaseContext;

namespace Tracks.Data.Repositories;

public class GenreRepository(TracksDbContext context) : IGenreRepository
{
    public async Task<Genre> AddGenre(Genre genre)
    {
        var result = await context.AddAsync(genre);
        await context.SaveChangesAsync();
        return result.Entity;
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
