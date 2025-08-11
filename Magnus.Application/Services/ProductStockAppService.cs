using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class ProductStockAppService(
    IProductStockService productStockService,
    IUnitOfWork unitOfWork) : IProductStockAppService
{
    public async Task<IEnumerable<ProductStockResponse>> GetProductStocksByFilterAsync(Guid productId, int warehouseId,
        bool all, CancellationToken cancellationToken)
    {
        if (all)
            return (await unitOfWork.ProductStocks.GetAllByExpressionAsync(
                x => x.Amount > 0 &&
                     productId == x.ProductId, cancellationToken)).MapToResponse();

        return (await unitOfWork.ProductStocks.GetAllByExpressionAsync(x =>
            x.Amount > 0 &&
            productId == x.ProductId && x.WarehouseId == warehouseId, cancellationToken)).MapToResponse();
    }

    public async Task CreateProductStockMovementAsync(CreateProductStockRequest request,
        CancellationToken cancellationToken)
    {
        var productStock = request.MapToEntity();
        await productStockService.CreateProductStockMovementAsync(productStock, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetBalanceProductStocksAsync(Guid productId, int warehouseId,
        CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetByIdAsync(productId, cancellationToken);
        if (product == null)
            throw new EntityNotFoundException("Nenhum produto encontrado com esse Id");
        return await productStockService.GetProductStockAsync(productId, warehouseId, cancellationToken);
    }
}