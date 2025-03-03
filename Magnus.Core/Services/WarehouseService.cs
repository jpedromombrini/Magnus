using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class WarehouseService(
    IUnitOfWork unitOfWork) : IWarehouseService
{
    public async Task<Warehouse?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await unitOfWork.Warehouses.GetByExpressionAsync(x => x.UserId == userId, cancellationToken);
    }
}