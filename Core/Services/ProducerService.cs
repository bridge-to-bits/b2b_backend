﻿using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mapping;
using Core.Models;
using Core.Responses;

namespace Core.Services;

public class ProducerService (IProducerRepository producerRepository, IUserService userService) : IProducerService
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

    public async Task<IEnumerable<ProducerRelatedPerformerResponse>> GetProducerRelatedPerformers(Guid producerId)
    {
        var relatedPerformers = await producerRepository.GetProducerRelatedPerformers(producerId);
        List<ProducerRelatedPerformerResponse> result = [];

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