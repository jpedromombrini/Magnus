using Magnus.Core.Entities;

namespace Magnus.Core.Servicos;

public interface ITransferWarehouseService
{
    Task ConfirmTransferWarehouse(TransferWarehouseItem transferWarehouseItem, Guid transferId, int originWarehouseId, int destinationWarehouseId, CancellationToken cancellationToken);
    Task CancelTransferWarehouse(TransferWarehouseItem transferWarehouseItem, Guid transferId, int originWarehouseId, int destinationWarehouseId, CancellationToken cancellationToken);
}