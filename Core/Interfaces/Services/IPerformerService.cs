using Core.DTOs.Users;
using Core.Models;
using Core.Responses;

namespace Core.Interfaces.Services;

public interface IPerformerService
{
    public Task<PerformersResponse> GetPerformers(QueryAllUsersDTO queryAllUsersDTO);
    public Task<Performer> GetPerformer(Guid performerId);
}
