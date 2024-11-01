using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Core.Interfaces;
using Users.Core.Models;
using Users.Data.DatabaseContext;

namespace Users.Data.Repositories
{
    public class UserRepository(UsersDbContext context) : IUserRepository
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
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUser( 
            Expression<Func<User, bool>> predicate,
            params Expression<Func<User, object>>[] includes)
        {
            IQueryable<User> query = context.Users.AsNoTracking();

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<User?> GetUserWithRolesAndGrants(Expression<Func<User, bool>> predicate)
        {
            return await context.Users
                .AsNoTracking()
                .Include(user => user.Roles)
                .ThenInclude(role => role.Grants)
                .FirstOrDefaultAsync(predicate);       
        }

        public async Task<IEnumerable<User>> GetUsers(
            Expression<Func<User, bool>> predicate,
            params Expression<Func<User, object>>[] includes)
        {
            IQueryable<User> query = context.Users.AsNoTracking();

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.Where(predicate).ToListAsync();
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

        public async Task<IEnumerable<Rating>> GetRatingsForUser(string userId)
        {
            return await context.Ratings
                .AsNoTracking()
                .Where(r => r.TargetUserId == Guid.Parse(userId))
                .ToListAsync();
        }
    }
}
