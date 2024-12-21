using Core.DTOs.Performers;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mapping;
using Core.Models;
using Core.Responses;
using System.Linq.Expressions;

namespace Core.Services;

public class PerformerService (IPerformerRepository performerRepository) : IPerformerService
{
    public Task<Performer> GetPerformer(Guid performerId)
    {
       return performerRepository.GetPerfomer(performerId);
    }

    public async Task<SendInvitationPerformersResponse> GetPerformers(QueryAllPerformersDTO queryAllUsersDTO)
    {
        Expression<Func<Performer, bool>> predicate = performer =>
            String.IsNullOrWhiteSpace(queryAllUsersDTO.Search)
            || performer.User.Username.Contains(queryAllUsersDTO.Search, StringComparison.CurrentCultureIgnoreCase);

        var performers = await performerRepository.GetPerformers(predicate);
        
        return performers.ToSendInvitationPerformersResponse();
    }
}
