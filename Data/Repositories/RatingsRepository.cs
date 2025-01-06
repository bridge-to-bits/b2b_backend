using Core.Interfaces.Repositories;
using Core.Models;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RatingsRepository(B2BDbContext context) : IRatingRepository
{
    public Task<Rating> GetGivenRating(Guid initiatorUserId, Guid targetUserId)
    {
       return context.Ratings
            .AsNoTracking()
            .Where(rating => 
                rating.InitiatorUserId == initiatorUserId 
                && rating.TargetUserId == targetUserId)
            .FirstAsync();
    }
}
