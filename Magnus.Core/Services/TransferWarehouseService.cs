using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class TransferWarehouseService(
    IUnitOfWork unitOfWork) : ITransferWarehouseService
{
    public async Task ConfirmTransferWarehouse(TransferWarehouseItem transferWarehouseItem, Guid transferId,
        int originWarehouseId,
        int destinationWarehouseId,
        CancellationToken cancellationToken)
    {
        var audits = CreateAuditProducts(transferWarehouseItem, transferId, originWarehouseId, destinationWarehouseId);
        var productStockOrigin = await unitOfWork.ProductStocks.GetByExpressionAsync(x =>
                x.WarehouseId == originWarehouseId &&
                transferWarehouseItem.ProductId == x.ProductId,
            cancellationToken);
        if (productStockOrigin is null)
            throw new EntityNotFoundException(transferWarehouseItem.ProductId);

        var productStockDestiny = await unitOfWork.ProductStocks.GetByExpressionAsync(x =>
                x.WarehouseId == destinationWarehouseId &&
                transferWarehouseItem.ProductId == x.ProductId,
            cancellationToken);

        productStockOrigin.DecreaseAmount(transferWarehouseItem.AutorizedAmount);
        unitOfWork.ProductStocks.Update(productStockOrigin);

        if (productStockDestiny is null)
        {
            productStockDestiny = new ProductStock(transferWarehouseItem.ProductId,
                transferWarehouseItem.AutorizedAmount, destinationWarehouseId);
            productStockDestiny.SetWarehouseName(transferWarehouseItem.TransferWarehouse.WarehouseDestinyName);
            await unitOfWork.ProductStocks.AddAsync(productStockDestiny, cancellationToken);
        }
        else
        {
            productStockDestiny.IncreaseAmount(transferWarehouseItem.AutorizedAmount);
            unitOfWork.ProductStocks.Update(productStockDestiny);
        }

        await unitOfWork.AuditProducts.AddRangeAsync(audits, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task CancelTransferWarehouse(TransferWarehouseItem transferWarehouseItem, Guid transferId,
        int originWarehouseId,
        int destinationWarehouseId, CancellationToken cancellationToken)
    {
        var productStockDestiny = await unitOfWork.ProductStocks.GetByExpressionAsync(x =>
                x.WarehouseId == destinationWarehouseId &&
                transferWarehouseItem.ProductId == x.ProductId,
            cancellationToken);

        if (productStockDestiny is null)
            throw new EntityNotFoundException(transferWarehouseItem.ProductId);

        productStockDestiny.DecreaseAmount(transferWarehouseItem.AutorizedAmount);
        unitOfWork.ProductStocks.Update(productStockDestiny);

        var productStockOrigin = await unitOfWork.ProductStocks.GetByExpressionAsync(x =>
                x.WarehouseId == originWarehouseId &&
                transferWarehouseItem.ProductId == x.ProductId,
            cancellationToken);
        if (productStockOrigin is null)
        {
            var productStock = new ProductStock(transferWarehouseItem.ProductId,
                transferWarehouseItem.AutorizedAmount, destinationWarehouseId);
            productStock.SetWarehouseName(transferWarehouseItem.TransferWarehouse.WarehouseDestinyName);

            await unitOfWork.ProductStocks.AddAsync(productStock, cancellationToken);
        }
        else
        {
            productStockOrigin.IncreaseAmount(transferWarehouseItem.AutorizedAmount);
            unitOfWork.ProductStocks.Update(productStockOrigin);
        }

        var audits = await unitOfWork.AuditProducts.GetAllByExpressionAsync(
            x => x.TransferhouseId == transferId && x.ProductId == transferWarehouseItem.ProductId, cancellationToken);
        unitOfWork.AuditProducts.RemoveRange(audits);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public void UpdateTransferWarehouseItem(TransferWarehouse transferWarehouse,
        IEnumerable<TransferWarehouseItem> transferWarehouseItems)
    {
        foreach (var item in transferWarehouseItems)
        {
            var existingItem = transferWarehouse.Items.SingleOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.SetRequestedAmount(item.RequestedAmount);
                existingItem.SetAutorizedAmount(item.AutorizedAmount);
                existingItem.SetStatus(item.Status);
                existingItem.SetTransferWarehouse(transferWarehouse);
                existingItem.SetTransferWarehouseId(transferWarehouse.Id);
            }
            else
            {
                item.SetTransferWarehouse(transferWarehouse);
                item.SetTransferWarehouseId(transferWarehouse.Id);
                transferWarehouse.AddItem(item);
            }
        }

        var itemsToRemove = transferWarehouse.Items
            .Where(existingItem => !transferWarehouseItems.Any(item => item.ProductId == existingItem.ProductId))
            .ToList();
        foreach (var itemToRemove in itemsToRemove) transferWarehouse.RemoveItem(itemToRemove);
    }

    private List<AuditProduct> CreateAuditProducts(TransferWarehouseItem transferWarehouseItem, Guid transferId,
        int originWarehouseId,
        int destinationWarehouseId)
    {
        var auditIn = new AuditProduct(transferWarehouseItem.ProductId, DateTime.Now, 0,
            transferWarehouseItem.AutorizedAmount,
            0, AuditProductType.In, null, 0, destinationWarehouseId, transferId, null, null);
        var auditOut = new AuditProduct(transferWarehouseItem.ProductId, DateTime.Now, 0,
            transferWarehouseItem.AutorizedAmount,
            0, AuditProductType.Out, null, 0, originWarehouseId, transferId, null, null);
        List<AuditProduct> audits = [auditIn, auditOut];
        return audits;
    }
}