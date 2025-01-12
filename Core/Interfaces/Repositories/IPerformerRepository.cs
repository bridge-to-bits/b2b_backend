using Core.Models;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories;

public interface IPerformerRepository
{
    public Task<Performer> GetPerfomer(Expression<Func<Performer, bool>> predicate);
    public Task<bool> Exist(Guid perfomerId);
    Task<List<Performer>> GetPerformers(Expression<Func<Performer, bool>> predicate);
    Task<List<FavoritePerformer>> GetfavoritePerformers(Guid userId);
    Task<List<User>> GetPerformersRelatedUsers(Expression<Func<Performer, bool>> predicate);
    Task<bool> IsFavoritePerformer(Guid userId, Guid performerId);
    void AddFavoritePerformer(FavoritePerformer favoritePerformer);
    void RemoveFavoritePerformer(Guid userId, Guid performerId);
}
