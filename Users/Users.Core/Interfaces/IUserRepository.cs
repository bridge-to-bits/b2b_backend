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

    public Task<List<User>> GetPaginationUsers(
        Expression<Func<User, bool>> predicate,
        IEnumerable<Expression<Func<User, object>>> includes = null,
        string sortBy = null,
        bool sortDescending = false,
        int skip = 0,
        int take = 10);
    public Task<User?> GetUserWithRolesAndGrants(Expression<Func<User, bool>> predicate);
    public Task CreateGrants(IEnumerable<Grant> grants);
    public Task CreateRole(Role role);
    public Task<User> CreateUser(User user);
    public Task<T> AttachEntityToUser<T>(string userId) where T : class, new();
    public Task AddRating(Rating rating);
    public Task<List<Rating>> GetRatingsForUser(string userId);
    public Task<int> Count(Expression<Func<User, bool>> predicate);
    public Task SaveAsync();
    public Task<User?> GetUserForUpdate(
        Expression<Func<User, bool>> predicate,
        params Expression<Func<User, object>>[] includes);
}
