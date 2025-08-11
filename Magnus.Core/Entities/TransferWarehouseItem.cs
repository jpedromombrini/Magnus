using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class TransferWarehouseItem : EntityBase
{
    private TransferWarehouseItem()
    {
    }

    public TransferWarehouseItem(Guid productId, int productInternalCode, string productName, int requestedAmount,
        Guid transferWarehouseId, TransferWarehouseItemStatus status, TransferWarehouse transferWarehouse)
    {
        SetProductId(productId);
        SetProductInternalCode(productInternalCode);
        SetProductName(productName);
        SetRequestedAmount(requestedAmount);
        SetTransferWarehouseId(transferWarehouseId);
        SetStatus(status);
        SetStatus(status);
        SetTransferWarehouse(transferWarehouse);
    }

    public Guid ProductId { get; private set; }
    public int ProductInternalCode { get; private set; }
    public string ProductName { get; private set; }
    public int RequestedAmount { get; private set; }
    public int AutorizedAmount { get; private set; }
    public Guid TransferWarehouseId { get; private set; }
    public TransferWarehouseItemStatus Status { get; private set; }
    public TransferWarehouse TransferWarehouse { get; private set; }

    public void SetProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Informe o Id do produto");
        ProductId = productId;
    }

    public void SetProductInternalCode(int productInternalCode)
    {
        if (productInternalCode <= 0)
            throw new ArgumentException("Informe o código do produto");
        ProductInternalCode = productInternalCode;
    }

    public void SetProductName(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Informe o nome do produto");
        ProductName = productName;
    }

    public void SetRequestedAmount(int requestedAmount)
    {
        if (requestedAmount <= 0)
            throw new ArgumentException("Informe a quantidade solicitada do produto");
        RequestedAmount = requestedAmount;
    }

    public void SetAutorizedAmount(int autorizedAmount)
    {
        AutorizedAmount = autorizedAmount;
    }

    public void SetTransferWarehouseId(Guid transferWarehouseId)
    {
        if (transferWarehouseId == Guid.Empty)
            throw new ArgumentException("Informe o Id do depósito");
        TransferWarehouseId = transferWarehouseId;
    }

    public void SetStatus(TransferWarehouseItemStatus status)
    {
        Status = status;
    }

    public void SetTransferWarehouse(TransferWarehouse transferWarehouse)
    {
        TransferWarehouse = transferWarehouse;
    }
}