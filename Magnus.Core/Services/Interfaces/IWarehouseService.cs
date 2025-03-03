using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IWarehouseService
{
    Task<Warehouse?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}