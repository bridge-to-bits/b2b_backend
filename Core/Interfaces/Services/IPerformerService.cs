using Core.DTOs.Users;
using Core.Models;
using Core.Responses;
using System.Linq.Expressions;

namespace Core.Interfaces.Services;

public interface IPerformerService
{
    public Task<PerformersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO);
    public Task<Performer> GetPerformer(Expression<Func<Performer, bool>> predicate);
}
