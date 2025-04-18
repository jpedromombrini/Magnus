using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IBarService
{
    Task AddRangeAsync(IEnumerable<Bar> bars, CancellationToken cancellationToken);
    Task<IEnumerable<Bar>> GetByProductId(Guid productId, CancellationToken cancellationToken);
    Task RemoveRangeAsync(Guid productId, CancellationToken cancellationToken);
}