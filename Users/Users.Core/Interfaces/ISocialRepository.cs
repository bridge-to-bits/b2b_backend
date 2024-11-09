using System.Linq.Expressions;
using Users.Core.Models;

namespace Users.Core.Interfaces;

public interface ISocialRepository
{
    public Task<Social> AddSocial(Social social);
    public Task<Social> RemoveSocial(Expression<Func<Social, bool>> predicate);
    public Task<List<Social>> GetSocials(Expression<Func<Social, bool>> predicate);
}
