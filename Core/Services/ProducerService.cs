using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mapping;
using Core.Models;
using Core.Responses;

namespace Core.Services;

public class ProducerService(IProducerRepository producerRepository, IUserService userService) : IProducerService
{
    public Task AddRelatedPerformer(Guid producerId, Performer relatedPerformer)
    {
        return producerRepository.AddRelatedPerformer(producerId, relatedPerformer);
    }

    public Task<bool> Exist(Guid producerId)
    {
        return producerRepository.Exist(producerId);
    }

    public Task<Producer> GetProducer(Guid producerId)
    {
        return producerRepository.GetProducer(producerId);
    }

    public async Task<IEnumerable<ProducerRelatedPerformerResponse>> GetProducerRelatedPerformers(Guid userId)
    {
        var userType = await userService.GetUserType(userId);
        if (userType != UserType.Producer)
        {
            throw new Exception("User is not a producer");
        }

        var relatedPerformers = await producerRepository.GetProducerRelatedPerformers(userId);
        List<ProducerRelatedPerformerResponse> result = [];
        if (relatedPerformers == null)
        {
            return null;
        }

        foreach (var relatedPerformer in relatedPerformers)
        {
            var rating = await userService.GetUserAverageRating(relatedPerformer.UserId.ToString());
            var relatedPerformerResponse = relatedPerformer.ToProducerRelatedPerformerResponse();
            relatedPerformerResponse.Rating = rating;
            result.Add(relatedPerformerResponse);
        }

        return result;
    }
}
