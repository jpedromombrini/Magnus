using System.Linq.Expressions;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface IProductStockAppService
{
    Task<IEnumerable<ProductStockResponse>> GetProductStocksByFilterAsync(Guid productId, int warehouseId, bool all, CancellationToken cancellationToken);
    Task<decimal> GetBalanceProductStocksAsync(Guid productId, int warehouseId, CancellationToken cancellationToken);
}