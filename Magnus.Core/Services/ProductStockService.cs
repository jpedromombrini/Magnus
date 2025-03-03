using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class ProductStockService(
    IUnitOfWork unitOfWork) : IProductStockService
{
    public async Task<decimal> GetProductStockAsync(Guid productId, int warehouseId,
        CancellationToken cancellationToken)
    {
        var productStocks = await unitOfWork.ProductStocks.GetAllByExpressionAsync(x => x.Amount > 0 &&
            productId == x.ProductId &&
            x.WarehouseId == warehouseId, cancellationToken);
        return productStocks.Sum(x => x.Amount);
    }

    public async Task SubtractProductStockAsync(Guid productId, int warehouseId, int amount,
        CancellationToken cancellationToken)
    {
        var stock = await unitOfWork.ProductStocks.GetByExpressionAsync(
            x => x.ProductId == productId && x.WarehouseId == warehouseId, cancellationToken);
        if(stock == null)
            throw new EntityNotFoundException("Produto não encontrado");
        if(stock.Amount < amount)
            throw new BusinessRuleException("Estoque insuficiente");
        stock.DecreaseAmount(amount);
        unitOfWork.ProductStocks.Update(stock);
    }

    public async Task IncrementProductStockAsync(Guid productId, int warehouseId, int amount,
        CancellationToken cancellationToken)
    {
        var stock = await unitOfWork.ProductStocks.GetByExpressionAsync(
            x => x.ProductId == productId && x.WarehouseId == warehouseId, cancellationToken);
        if(stock == null)
            throw new EntityNotFoundException("Produto não encontrado");
        stock.IncreaseAmount(amount);
        unitOfWork.ProductStocks.Update(stock);
    }
}