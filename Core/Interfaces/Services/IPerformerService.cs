using Core.DTOs.Performers;
using Core.Models;
using Core.Responses;

namespace Core.Interfaces.Services;

public interface IPerformerService
{
    public Task<SendInvitationPerformersResponse> GetPerformers(QueryAllPerformersDTO queryAllUsersDTO);
    public Task<Performer> GetPerformer(Guid performerId);
}
