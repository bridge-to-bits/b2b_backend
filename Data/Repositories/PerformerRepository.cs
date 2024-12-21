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

    public Task<Performer> GetPerfomer (Guid perfomerId)
    {
        return context.Performers.AsNoTracking()
            .Include(p => p.User)
            .FirstAsync(perfomer => perfomer.Id == perfomerId);
    }
}
