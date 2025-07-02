using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class StockMovement : EntityBase
{
    public StockMovement(Guid productId, int amount, AuditProductType auditProductType,
        int warehouseId, string warehouseName, Guid userId, string observation)
    {
        SetProductId(productId);
        SetAmount(amount);
        SetAuditProductType(auditProductType);
        SetWarehouseId(warehouseId);
        SetWarehouseName(warehouseName);
        SetUserId(userId);
        SetObservation(observation);
    }

    private StockMovement()
    {
    }

    public DateTime CreatAt { get; private set; } = DateTime.Now;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Amount { get; private set; }
    public AuditProductType AuditProductType { get; private set; }
    public int WarehouseId { get; private set; }
    public string WarehouseName { get; private set; }
    public Guid UserId { get; private set; }
    public string Observation { get; private set; }

    public void SetProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Id do produto não pode ser vazio");
        ProductId = productId;
    }

    public void SetProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        Product = product;
    }

    public void SetAmount(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("informe uma quantidade maior que zero");
        Amount = amount;
    }

    public void SetAuditProductType(AuditProductType auditProductType)
    {
        AuditProductType = auditProductType;
    }

    public void SetWarehouseId(int warehouseId)
    {
        WarehouseId = warehouseId;
    }

    public void SetWarehouseName(string warehouseName)
    {
        if (string.IsNullOrEmpty(warehouseName))
            throw new ArgumentException("Informe um nome para o depósito");
        WarehouseName = warehouseName.ToUpper();
    }

    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Informe o id do usuário");
        UserId = userId;
    }

    public void SetObservation(string observation)
    {
        Observation = observation;
    }
}