using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Core.Interfaces;
using Users.Core.Models;
using Users.Data.DatabaseContext;

namespace Users.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _context;

        public UserRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.AsNoTracking().Where(predicate).FirstAsync();
        }

        public async Task<IEnumerable<User>> GetUsers(Expression<Func<User, bool>> predicate)
        {
           return await _context.Users.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
