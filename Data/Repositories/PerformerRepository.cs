using Core.Interfaces.Repositories;
using Core.Models;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class PerformerRepository(B2BDbContext context) : IPerformerRepository
{
    public Task<bool> Exist(Guid perfomerId)
    {
        return context.Performers.AnyAsync(perfomer => perfomer.Id == perfomerId);
    }

    public Task<List<Performer>> GetPerformers(Expression<Func<Performer, bool>> predicate)
    {
        return context.Performers
                .AsNoTracking()
                .Where(predicate)
                .Include(performer => performer.User)
                    .ThenInclude(user => user.Genres)
                .Include(performer => performer.User)
                    .ThenInclude(user => user.Socials)
                .Include(performer => performer.User)
                    .ThenInclude(user => user.ReceivedRatings)
                .ToListAsync();
    }

    public Task<Performer> GetPerfomer (Expression<Func<Performer, bool>> predicate)
    {
        return context.Performers.AsNoTracking()
            .Include(p => p.User)
            .FirstAsync(predicate);
    }

    public Task<List<FavoritePerformer>> GetfavoritePerformers(Guid userId)
    {
        return context.FavoritePerformers
            .AsNoTracking()
            .Where(favoritePerformer => favoritePerformer.UserId == userId)
            .ToListAsync();
    }

    public Task<List<User>> GetPerformersRelatedUsers(Expression<Func<Performer, bool>> predicate)
    {
        return context.Performers
            .AsNoTracking()
            .Where(predicate)
            .Include(performer => performer.User)
            .Select(performer => performer.User)
            .ToListAsync();
    }

    public Task<bool> IsFavoritePerformer(Guid userId, Guid performerId)
    {
        return context.FavoritePerformers.AnyAsync(favPerformer =>
            favPerformer.UserId == userId && favPerformer.PerformerId == performerId);
    }

    public async void AddFavoritePerformer(FavoritePerformer favoritePerformer)
    {
        var exist = await IsFavoritePerformer(favoritePerformer.UserId, favoritePerformer.PerformerId);
        if(!exist)
        {
            context.FavoritePerformers.Add(favoritePerformer);
            await context.SaveChangesAsync();
        }
    }

    public async void RemoveFavoritePerformer(Guid userId, Guid performerId)
    {
        var favoritePerformer = await context.FavoritePerformers.FirstOrDefaultAsync(favPerformer => 
            favPerformer.PerformerId == performerId && favPerformer.UserId == userId);

        if(favoritePerformer != null)
        {
            context.FavoritePerformers.Remove(favoritePerformer);
            await context.SaveChangesAsync();
        }
    }
}
