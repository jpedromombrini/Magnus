using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class ProductStockAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IProductStockAppService
{
    public async Task<IEnumerable<ProductStockResponse>> GetProductStocksByFilterAsync(Guid productId, int warehouseId, bool all, CancellationToken cancellationToken)
    {
        if(all)
            return mapper.Map<IEnumerable<ProductStockResponse>>(await unitOfWork.ProductStocks.GetAllByExpressionAsync(x => x.Amount > 0 &&
                productId == x.ProductId, cancellationToken));
        
        return mapper.Map<IEnumerable<ProductStockResponse>>(await unitOfWork.ProductStocks.GetAllByExpressionAsync(x => x.Amount > 0 &&
            productId == x.ProductId && x.WarehouseId == warehouseId, cancellationToken));
    }

    public async Task<decimal> GetBalanceProductStocksAsync(Guid productId, int warehouseId, CancellationToken cancellationToken)
    {
        var productStocks = await unitOfWork.ProductStocks.GetAllByExpressionAsync(x => x.Amount > 0 &&
            productId == x.ProductId &&
            x.WarehouseId == warehouseId, cancellationToken);
        return productStocks.Sum(x => x.Amount);
    }
}