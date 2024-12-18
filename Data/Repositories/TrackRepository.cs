using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Models;
using Data.DatabaseContext;
using Core.Interfaces.Repositories;

namespace Data.Repositories;

public class TrackRepository(B2BDbContext context) : ITrackRepository
{
    public async Task<Track> CreateTrack(Track track)
    {
        foreach (var genre in track.Genres)
        {
            context.Entry(genre).State = EntityState.Unchanged;
        }

        var result = await context.AddAsync(track);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<IEnumerable<Genre>> GetTrackGenres(string trackId)
    {
        var trackIdGuid = Guid.Parse(trackId);
        var track = await context.Tracks
            .Include(t => t.Genres)
            .FirstOrDefaultAsync(t => t.Id == trackIdGuid);

        return track?.Genres ?? Enumerable.Empty<Genre>();
    }

    public Task<Track?> GetTrack(
        Expression<Func<Track, bool>> predicate,
        params Expression<Func<Track, object>>[] includes)
    {
        IQueryable<Track> query = context.Tracks.AsNoTracking();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query.FirstOrDefaultAsync(predicate);
    }

    public Task<List<Track>> GetTracks(
        Expression<Func<Track, bool>> predicate,
        params Expression<Func<Track, object>>[] includes)
    {
        IQueryable<Track> query = context.Tracks.AsNoTracking();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query.Where(predicate).ToListAsync();
    }

    public Task RemoveTrack(Track track)
    {
        context.Tracks.Remove(track);
        return context.SaveChangesAsync();
    }

    public Task<List<Genre>> GetGenres(IEnumerable<string> trackIds)
    {
        return context.Genres
            .AsNoTracking()
            .Where(genre => trackIds.Contains(genre.Id.ToString()))
            .ToListAsync();
    }

    public async Task<List<Track>> GetPaginationTracks(
        Expression<Func<Track, bool>> predicate,
        IEnumerable<Expression<Func<Track, object>>> includes = null,
        string sortBy = null,
        bool sortDescending = false)
    {
        IQueryable<Track> query = context.Tracks.AsNoTracking();

        query = query.Where(predicate);

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            query = sortDescending
                ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                : query.OrderBy(e => EF.Property<object>(e, sortBy));
        }

        return await query.ToListAsync();
    }

    public Task<bool> Exist(string trackId)
    {
        return context.Tracks.AnyAsync(t => t.Id == Guid.Parse(trackId));
    }
}
