using Core.Interfaces.Repositories;
using Core.Models;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProducerRepository(B2BDbContext context) : IProducerRepository
{
    public async Task AddRelatedPerformer(Guid producerId, Performer relatedPerformer)
    {
        var producer = await context.Producers.FindAsync(producerId);
        producer?.RelatedPerformers.Add(relatedPerformer);
        await context.SaveChangesAsync();
    }

    public Task<bool> Exist(Guid producerId)
    {
        return context.Producers.AsNoTracking().AnyAsync(p => p.Id == producerId);
    }

    public Task<Producer> GetProducer(Guid producerId)
    {
        return context.Producers.AsNoTracking().Include(p => p.User).FirstAsync(p => p.Id == producerId);
    }

    public async Task<IEnumerable<Performer>> GetProducerRelatedPerformers(Guid userId)
    {
        var producer = await context.Producers
            .AsNoTracking()
            .Include(producer => producer.RelatedPerformers)
            .ThenInclude(performer => performer.User)
            .ThenInclude(user => user.Genres)
            .FirstAsync(producer => producer.UserId == userId);

        return [.. producer.RelatedPerformers];
    }
}
