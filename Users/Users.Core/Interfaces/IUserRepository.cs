using System.Linq.Expressions;
using Users.Core.Models;

namespace Users.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);

        public Task<User> GetUser(Expression<Func<User, bool>> predicate);
        public Task<IEnumerable<User>> GetUsers(Expression<Func<User, bool>> predicate);
    }
}
