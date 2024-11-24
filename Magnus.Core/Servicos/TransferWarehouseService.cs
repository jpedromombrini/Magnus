using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Core.Servicos;

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
            transferWarehouseItem.ProductId == x.ProductId &&
            x.ValidityDate == transferWarehouseItem.Validity, cancellationToken);
        if (productStockOrigin is null)
            throw new EntityNotFoundException(transferWarehouseItem.ProductId);

        productStockOrigin.DecreaseAmount(transferWarehouseItem.Amount);
        unitOfWork.ProductStocks.UpdateAsync(productStockOrigin);

        var productStockDestiny = await unitOfWork.ProductStocks.GetByExpressionAsync(x =>
            x.WarehouseId == destinationWarehouseId &&
            transferWarehouseItem.ProductId == x.ProductId &&
            x.ValidityDate == transferWarehouseItem.Validity, cancellationToken);
        if (productStockDestiny is null)
        {
            productStockDestiny = new ProductStock(transferWarehouseItem.ProductId, transferWarehouseItem.Validity,
                transferWarehouseItem.Amount, destinationWarehouseId,
                transferWarehouseItem.TransferWarehouse.WarehouseDestinyName);
            await unitOfWork.ProductStocks.AddAsync(productStockDestiny, cancellationToken);
        }
        else
        {
            productStockDestiny.IncreaseAmount(transferWarehouseItem.Amount);
            unitOfWork.ProductStocks.UpdateAsync(productStockDestiny);
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
            transferWarehouseItem.ProductId == x.ProductId &&
            x.ValidityDate == transferWarehouseItem.Validity, cancellationToken);

        if (productStockDestiny is null)
            throw new EntityNotFoundException(transferWarehouseItem.ProductId);

        productStockDestiny.DecreaseAmount(transferWarehouseItem.Amount);
        unitOfWork.ProductStocks.UpdateAsync(productStockDestiny);

        var productStockOrigin = await unitOfWork.ProductStocks.GetByExpressionAsync(x =>
            x.WarehouseId == originWarehouseId &&
            transferWarehouseItem.ProductId == x.ProductId &&
            x.ValidityDate == transferWarehouseItem.Validity, cancellationToken);
        if (productStockOrigin is null)
        {
            var productStock = new ProductStock(transferWarehouseItem.ProductId, transferWarehouseItem.Validity,
                transferWarehouseItem.Amount, destinationWarehouseId,
                transferWarehouseItem.TransferWarehouse.WarehouseDestinyName);

            await unitOfWork.ProductStocks.AddAsync(productStock, cancellationToken);
        }
        else
        {
            productStockOrigin.IncreaseAmount(transferWarehouseItem.Amount);
            unitOfWork.ProductStocks.UpdateAsync(productStockOrigin);
        }

        var audits = await unitOfWork.AuditProducts.GetAllByExpressionAsync(
            x => x.TransferhouseId == transferId && x.ProductId == transferWarehouseItem.ProductId, cancellationToken);
        unitOfWork.AuditProducts.RemoveRange(audits);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private List<AuditProduct> CreateAuditProducts(TransferWarehouseItem transferWarehouseItem, Guid transferId,
        int originWarehouseId,
        int destinationWarehouseId)
    {
        var auditIn = new AuditProduct(transferWarehouseItem.ProductId, DateTime.Now, 0, transferWarehouseItem.Amount,
            0, AuditProductType.In, null, 0, destinationWarehouseId, transferId, null, transferWarehouseItem.Validity);
        var auditOut = new AuditProduct(transferWarehouseItem.ProductId, DateTime.Now, 0, transferWarehouseItem.Amount,
            0, AuditProductType.Out, null, 0, originWarehouseId, transferId, null, transferWarehouseItem.Validity);
        List<AuditProduct> audits = [auditIn, auditOut];
        return audits;
    }
}