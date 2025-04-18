using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IBarRepository : IRepository<Bar>
{
    Task AddRangeAsync(IEnumerable<Bar> bars, CancellationToken cancellationToken);
    void RemoveRange(IEnumerable<Bar> bars);
}