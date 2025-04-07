using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ITransferWarehouseService
{
    Task ConfirmTransferWarehouse(TransferWarehouseItem transferWarehouseItem, Guid transferId, int originWarehouseId,
        int destinationWarehouseId, CancellationToken cancellationToken);

    Task CancelTransferWarehouse(TransferWarehouseItem transferWarehouseItem, Guid transferId, int originWarehouseId,
        int destinationWarehouseId, CancellationToken cancellationToken);

    void UpdateTransferWarehouseItem(TransferWarehouse transferWarehouse,
        IEnumerable<TransferWarehouseItem> transferWarehouseItems);
}