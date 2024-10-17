using Users.Core.Models;

namespace Users.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
    }
}
