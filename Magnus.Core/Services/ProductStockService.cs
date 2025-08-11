using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class ProductStockService(
    IUnitOfWork unitOfWork) : IProductStockService
{
    public async Task<int> GetProductStockAsync(Guid productId, int warehouseId,
        CancellationToken cancellationToken)
    {
        var productStocks = await unitOfWork.ProductStocks.GetAllByExpressionAsync(x => x.Amount > 0 &&
            productId == x.ProductId &&
            x.WarehouseId == warehouseId, cancellationToken);
        return productStocks.Sum(x => x.Amount);
    }

    public async Task SubtractProductStockAsync(Guid productId, Warehouse warehouse, int amount,
        CancellationToken cancellationToken)
    {
        var stock = await unitOfWork.ProductStocks.GetByExpressionAsync(
            x => x.ProductId == productId && x.WarehouseId == warehouse.Code, cancellationToken);
        if (stock == null)
            throw new EntityNotFoundException("Produto não encontrado");
        if (stock.Amount < amount)
            throw new BusinessRuleException("Estoque insuficiente");
        stock.DecreaseAmount(amount);
        unitOfWork.ProductStocks.Update(stock);
    }

    public async Task IncrementProductStockAsync(Guid productId, Warehouse warehouse, int amount,
        CancellationToken cancellationToken)
    {
        var stock = await unitOfWork.ProductStocks.GetByExpressionAsync(
            x => x.ProductId == productId && x.WarehouseId == warehouse.Code, cancellationToken);
        if (stock == null)
        {
            var product = await unitOfWork.Products.GetByIdAsync(stock.ProductId, cancellationToken);
            if (product is null)
                throw new BusinessRuleException("nenhum produto encontrado com esse Id");
            stock = new ProductStock(productId, amount, warehouse.Code);
            stock.SetWarehouseName(warehouse.Name);
            stock.SetProduct(product);
        }
        else
        {
            stock.IncreaseAmount(amount);
        }

        unitOfWork.ProductStocks.Update(stock);
    }

    public async Task CreateProductStockMovementAsync(ProductStock productStock, CancellationToken cancellationToken)
    {
        var (product, wareHouse) = await ValidadeProductStock(productStock, cancellationToken);
        await CheckStockProduct(productStock, cancellationToken);
        await CreateAuditProduck(productStock, cancellationToken, product, wareHouse);
    }

    private async Task CreateAuditProduck(ProductStock productStock, CancellationToken cancellationToken,
        Product? product,
        Warehouse wareHouse)
    {
        var audit = new AuditProduct(product.Id, DateTime.Now, 0, productStock.Amount, 0m, AuditProductType.In, null,
            998, wareHouse.Code, null, null, null);
        await unitOfWork.AuditProducts.AddAsync(audit, cancellationToken);
    }

    private async Task CheckStockProduct(ProductStock productStock, CancellationToken cancellationToken)
    {
        var stockExists = await unitOfWork.ProductStocks.GetByExpressionAsync(
            x => x.ProductId == productStock.ProductId && x.WarehouseId == productStock.WarehouseId, cancellationToken);
        if (stockExists is null)
        {
            await unitOfWork.ProductStocks.AddAsync(productStock, cancellationToken);
        }
        else
        {
            stockExists.IncreaseAmount(productStock.Amount);
            unitOfWork.ProductStocks.Update(stockExists);
        }
    }

    private async Task<(Product? product, Warehouse? wareHouse)> ValidadeProductStock(ProductStock productStock,
        CancellationToken cancellationToken)
    {
        var product =
            await unitOfWork.Products.GetByExpressionAsync(x => x.Id == productStock.ProductId, cancellationToken);
        if (product == null)
            throw new BusinessRuleException("Nenhum produto encontrado com esse código");
        var wareHouse =
            await unitOfWork.Warehouses.GetByExpressionAsync(x => x.Code == productStock.WarehouseId,
                cancellationToken);
        if (wareHouse == null)
            throw new BusinessRuleException("Nenhum depósito encontrado com esse código");
        productStock.SetProduct(product);
        productStock.SetWarehouseName(wareHouse.Name);
        return (product, wareHouse);
    }
}