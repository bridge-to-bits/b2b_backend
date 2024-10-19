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

        public async Task<User?> GetUser(Expression<Func<User, bool>> predicate)
        {
            return await context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Grants)
                .Where(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers(Expression<Func<User, bool>> predicate)
        {
           return await context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Grants)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
