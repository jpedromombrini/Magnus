using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class AuditProduct : EntityBase
{
    public Guid ProductId { get; private set; }
    public DateTime RecordDate { get; private set; }
    public int Document { get; private set; }
    public int Amount { get; private set; }
    public decimal TotalValue { get; private set; }
    public AuditProductType Type { get; private set; }
    public Guid? SupplierId { get; private set; }
    public int Serie { get; private set; }
    public int WarehouseId { get; private set; }
    public Guid? TransferhouseId { get; private set; }
    public Guid? ClientId { get; private set; }
    public Guid? SaleId { get; private set; }
    private AuditProduct(){}

    public AuditProduct(Guid productId, DateTime recordDate, int document, int amount, decimal totalValue,
        AuditProductType type, Guid? supplierId, int serie, int warehouseId, Guid? transferhouseId, Guid? clientId,  Guid? saleId)
    {
        SetProductId(productId);
        SetRecordDate(recordDate);
        SetDocument(document);
        SetAmount(amount);
        SetTotalValue(totalValue);
        SetType(type);
        SetSupplierId(supplierId);
        SetSerie(serie);
        SetWarehouseId(warehouseId);
        SetTransferhouseId(transferhouseId);
        SetClientId(clientId);
        SetSaleId(saleId);
    }

    public void SetProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("O ProductId não pode ser vazio.");
        ProductId = productId;
    }

    public void SetRecordDate(DateTime recordDate)
    {
        RecordDate = recordDate;
    }

    public void SetDocument(int document)
    {
        if (document < 0)
            throw new ArgumentException("O número do documento deve ser maior que zero.");
        Document = document;
    }

    public void SetAmount(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");
        Amount = amount;
    }

    public void SetTotalValue(decimal totalValue)
    {
        if (totalValue < 0)
            throw new ArgumentException("O valor total não pode ser negativo.");
        TotalValue = totalValue;
    }

    public void SetType(AuditProductType type)
    {
        Type = type;
    }

    public void SetSupplierId(Guid? supplierId)
    {
        if (supplierId.HasValue && supplierId == Guid.Empty)
            throw new ArgumentException("O SupplierId não pode ser vazio.");
        SupplierId = supplierId;
    }

    public void SetSerie(int serie)
    {
        if (serie < 0)
            throw new ArgumentException("A série deve ser maior ou igual a zero.");
        Serie = serie;
    }

    public void SetWarehouseId(int warehouseId)
    {
        if (warehouseId < 0)
            throw new ArgumentException("O WarehouseId deve ser maior que zero.");
        WarehouseId = warehouseId;
    }

    public void SetTransferhouseId(Guid? transferhouseId)
    {
        if (transferhouseId.HasValue && transferhouseId == Guid.Empty)
            throw new ArgumentException("O TransferhouseId não pode ser vazio.");
        TransferhouseId = transferhouseId;
    }

    public void SetClientId(Guid? clientId)
    {
        if (clientId.HasValue && clientId == Guid.Empty)
            throw new ArgumentException("O ClientId não pode ser vazio.");
        ClientId = clientId;
    }

    public void SetSaleId(Guid? saleId)
    {
        if (saleId.HasValue && saleId == Guid.Empty)
            throw new ArgumentException("O ClientId não pode ser vazio.");
        SaleId = saleId;
    }
}