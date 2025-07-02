using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class StockMovementService(IUnitOfWork unitOfWork)
    : IStockMovementService
{
    public async Task CreateStockMovementAsync(StockMovement stockMovement, CancellationToken cancellationToken)
    {
        await unitOfWork.StockMovements.AddAsync(stockMovement, cancellationToken);
        var auditProduct = new AuditProduct(stockMovement.ProductId, DateTime.Now, 0, stockMovement.Amount, 0,
            stockMovement.AuditProductType, null, 0, stockMovement.WarehouseId, null, null, null);
        var productStock = await unitOfWork.ProductStocks.GetByExpressionAsync(
            x => x.ProductId == stockMovement.ProductId && x.WarehouseId == stockMovement.WarehouseId,
            cancellationToken);
        if (stockMovement.AuditProductType == AuditProductType.In)
        {
            if (productStock is null)
            {
                productStock = new ProductStock(stockMovement.ProductId, stockMovement.Amount,
                    stockMovement.WarehouseId, stockMovement.WarehouseName);
                await unitOfWork.ProductStocks.AddAsync(productStock, cancellationToken);
            }
            else
            {
                productStock.IncreaseAmount(stockMovement.Amount);
                unitOfWork.ProductStocks.Update(productStock);
            }
        }
        else
        {
            if (productStock is null) throw new BusinessRuleException("Não é possível subtrair estoque da mercadoria");
            productStock.DecreaseAmount(stockMovement.Amount);
        }

        
        await unitOfWork.AuditProducts.AddAsync(auditProduct, cancellationToken);
    }
}