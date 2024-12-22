using Core.Models;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories;

public interface IPerformerRepository
{
    public Task<Performer> GetPerfomer(Expression<Func<Performer, bool>> predicate);
    public Task<bool> Exist(Guid perfomerId);
    Task<List<Performer>> GetPerformers(Expression<Func<Performer, bool>> predicate);
}
