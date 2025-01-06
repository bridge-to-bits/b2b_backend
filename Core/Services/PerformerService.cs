using Core.DTOs.Users;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mapping;
using Core.Models;
using Core.Responses.Performers;
using System.Linq.Expressions;

namespace Core.Services;

public class PerformerService (IPerformerRepository performerRepository) : IPerformerService
{
    public Task<Performer> GetPerformer(Expression<Func<Performer, bool>> predicate)
    {
       return performerRepository.GetPerfomer(predicate);
    }

    public async Task<PerformersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO)
    {
        Expression<Func<Performer, bool>> predicate = performer =>
            String.IsNullOrWhiteSpace(queryAllUsersDTO.Search)
            || performer.User.Username.Contains(queryAllUsersDTO.Search, StringComparison.CurrentCultureIgnoreCase);

        var performers = await performerRepository.GetPerformers(predicate);
        
        return performers.ToSendInvitationPerformersResponse();
    }
}
