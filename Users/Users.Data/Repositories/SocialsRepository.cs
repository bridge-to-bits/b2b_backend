using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Core.Interfaces;
using Users.Core.Models;
using Users.Data.DatabaseContext;

namespace Users.Data.Repositories;

public class SocialsRepository(UsersDbContext context) : ISocialRepository
{
    public async Task<Social> AddSocial(Social social)
    {
        var createdSocial = await context.Socials.AddAsync(social);
        await context.SaveChangesAsync();
        return createdSocial.Entity;
    }

    public Task<List<Social>> GetSocials(Expression<Func<Social, bool>> predicate)
    {
        return context.Socials
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
    }

    public Task<Social> RemoveSocial(Expression<Func<Social, bool>> predicate)
    {
        return context.Socials
                .AsNoTracking()
                .FirstAsync(predicate);
    }
}
