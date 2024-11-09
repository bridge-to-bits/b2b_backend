using System.Linq.Expressions;
using Users.Core.Models;

namespace Users.Core.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetUser(
        Expression<Func<User, bool>> predicate,
        params Expression<Func<User, object>>[] includes);
    public Task<List<User>> GetUsers(
        Expression<Func<User, bool>> predicate,
        params Expression<Func<User, object>>[] includes);
    public Task<User?> GetUserWithRolesAndGrants(Expression<Func<User, bool>> predicate);
    public Task CreateGrants(IEnumerable<Grant> grants);
    public Task CreateRole(Role role);
    public Task<User> CreateUser(User user);
    public Task<T> AttachEntityToUser<T>(string userId) where T : class, new();
    public Task AddRating(Rating rating);
    public Task<List<Rating>> GetRatingsForUser(string userId);
}
