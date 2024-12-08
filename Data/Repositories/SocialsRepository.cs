using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models;

namespace Data.Repositories;

public class SocialsRepository(B2BDbContext context) : ISocialRepository
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
