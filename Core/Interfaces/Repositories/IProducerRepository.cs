using Core.Models;

namespace Core.Interfaces.Repositories;

public interface IProducerRepository
{
    public Task<bool> Exist(Guid producerId);
    public Task<Producer> GetProducer(Guid producerId);
    public Task<IEnumerable<Performer>> GetProducerRelatedPerformers(Guid producerId);
    public Task AddRelatedPerformer(Guid producerId, Performer relatedPerformer);
}
