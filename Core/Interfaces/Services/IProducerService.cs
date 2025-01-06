using Core.Models;
using Core.Responses.Performers;

namespace Core.Interfaces.Services;

public interface IProducerService
{
    public Task<bool> Exist(Guid producerId);
    public Task<Producer> GetProducer(Guid userId);
    public Task<IEnumerable<ProducerRelatedPerformerResponse>> GetProducerRelatedPerformers(Guid userId);
    public Task AddRelatedPerformer(Guid producerId, Performer relatedPerformer);
}
