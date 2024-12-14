using Core.Models;

namespace Core.Interfaces.Repositories;

public interface IPerformerRepository
{
    public Task<Performer> GetPerfomer(Guid perfomerId);
    public Task<bool> Exist(Guid perfomerId);
}
