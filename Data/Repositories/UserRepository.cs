using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Models;
using Core.Interfaces.Repositories;

namespace Data.Repositories;

public class UserRepository(B2BDbContext context) : IUserRepository
{
    public async Task CreateGrants(IEnumerable<Grant> grants)
    {
        await context.Grants.AddRangeAsync(grants);
        await context.SaveChangesAsync();
    }

    public async Task CreateRole(Role role)
    {
        await context.Roles.AddAsync(role);
        await context.SaveChangesAsync();
    }

    public async Task<User> CreateUser(User user)
    {
        var res = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return res.Entity;
    }

    public Task<User?> GetUser( 
        Expression<Func<User, bool>> predicate,
        params Expression<Func<User, object>>[] includes)
    {
        IQueryable<User> query = context.Users.AsNoTracking();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query.FirstOrDefaultAsync(predicate);
    }

    public Task<User?> GetUserWithRolesAndGrants(Expression<Func<User, bool>> predicate)
    {
        return context.Users
            .AsNoTracking()
            .Include(user => user.Roles)
            .ThenInclude(role => role.Grants)
            .FirstOrDefaultAsync(predicate);       
    }

    public Task<List<User>> GetUsers(
        Expression<Func<User, bool>> predicate,
        params Expression<Func<User, object>>[] includes)
    {
        IQueryable<User> query = context.Users.AsNoTracking();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query.Where(predicate).ToListAsync();
    }

    public async Task<T> AttachEntityToUser<T>(string userId) where T : class, new()
    {
        var entity = new T();
        if (entity is Producer producer)
        {
            producer.UserId = Guid.Parse(userId);
            await context.Producers.AddAsync(producer);
        }

        else if (entity is Performer performer)
        {
            performer.UserId = Guid.Parse(userId);
            await context.Performers.AddAsync(performer);
        }

        await context.SaveChangesAsync();
        return entity;
    }

    public async Task AddRating(Rating rating)
    {
        await context.Ratings.AddAsync(rating);
        await context.SaveChangesAsync();
    }

    public Task<List<Rating>> GetRatingsForUser(string userId)
    {
        return context.Ratings
            .AsNoTracking()
            .Where(r => r.TargetUserId == Guid.Parse(userId))
            .ToListAsync();
    }

    public Task<List<User>> GetPaginationUsers(
        Expression<Func<User, bool>> predicate, 
        IEnumerable<Expression<Func<User, object>>> includes = null, 
        string sortBy = null, 
        bool sortDescending = false,
        int skip = 0,
        int take = 10)
    {
        IQueryable<User> query = context.Users.AsNoTracking().Where(predicate);

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

        query = query.Skip(skip).Take(take);
        return query.ToListAsync();
    }

    public Task<int> Count(Expression<Func<User, bool>> predicate)
    {
        return context.Users.CountAsync(predicate);
    }

    public Task<User?> GetUserForUpdate(
        Expression<Func<User, bool>> predicate,
        params Expression<Func<User, object>>[] includes)
    {
        IQueryable<User> query = context.Users;

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query.FirstOrDefaultAsync(predicate);
    }
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public Task<User> GetUserWithFavoritePerformers(Guid userId)
    {
        return context.Users
            .AsNoTracking()
            .Include(user => user.FavoritePerformers)
            .ThenInclude(fp => fp.Performer)
            .ThenInclude(performer => performer.User)
            .ThenInclude(user => user.Socials)
            .FirstAsync(user => user.Id == userId);
    }
}
