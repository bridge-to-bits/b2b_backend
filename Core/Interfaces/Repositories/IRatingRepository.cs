using Core.Models;

namespace Core.Interfaces.Repositories;

public interface IRatingRepository
{
    public Task<Rating> GetGivenRating(Guid initiatorUserId, Guid targetUserId);
}
