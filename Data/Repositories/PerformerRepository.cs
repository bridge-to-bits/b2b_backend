using Core.Interfaces.Repositories;
using Core.Models;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PerformerRepository (B2BDbContext context): IPerformerRepository
{
    public Task<bool> Exist(Guid perfomerId)
    {
        return context.Performers.AnyAsync(perfomer => perfomer.Id == perfomerId);
    }

    public Task<Performer> GetPerfomer (Guid perfomerId)
    {
        return context.Performers.AsNoTracking()
            .Include(p => p.User)
            .FirstAsync(perfomer => perfomer.Id == perfomerId);
    }
}
