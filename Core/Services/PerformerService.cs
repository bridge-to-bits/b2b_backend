using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;

namespace Core.Services;

public class PerformerService (IPerformerRepository performerRepository) : IPerformerService
{
    public Task<Performer> GetPerformer(Guid performerId)
    {
       return performerRepository.GetPerfomer(performerId);
    }
}
