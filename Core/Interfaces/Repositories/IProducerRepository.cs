using Core.Models;

namespace Core.Interfaces.Repositories;

public interface IProducerRepository
{
    public Task<bool> Exist(Guid producerId);
    public Task<Producer> GetProducer(Guid userId);
    public Task<IEnumerable<Performer>> GetProducerRelatedPerformers(Guid userId);
    public Task AddRelatedPerformer(Guid producerId, Performer relatedPerformer);
}
