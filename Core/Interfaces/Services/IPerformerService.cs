using Core.Models;

namespace Core.Interfaces.Services;

public interface IPerformerService
{
    public Task<Performer> GetPerformer(Guid performerId);
}
