using Core.DTOs.Users;
using Core.Models;
using Core.Responses.Performers;
using System.Linq.Expressions;

namespace Core.Interfaces.Services;

public interface IPerformerService
{
    public Task<PerformersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO);
    public Task<Performer> GetPerformer(Expression<Func<Performer, bool>> predicate);

    Task<IEnumerable<FavoritePerformerResponse>> GetFavoritePerformers(Guid userId);
    Task<bool> IsFavoritePerformer(Guid userId, Guid performerId);
    void AddFavoritePerformer(Guid userId, Guid performerId);
    void RemoveFavoritePerformer(Guid userId, Guid performerId);
}
