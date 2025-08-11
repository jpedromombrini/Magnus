using Magnus.Core.Exceptions;

namespace Magnus.Core.Entities;

public class ProductStock : EntityBase
{
    public ProductStock(Guid productId, int amount, int warehouseId)
    {
        SetProductId(productId);
        SetAmount(amount);
        SetWarehouseId(warehouseId);
    }

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Amount { get; private set; }
    public int WarehouseId { get; private set; }
    public string WarehouseName { get; private set; }

    public void SetProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("O ProductId não pode ser vazio.");
        ProductId = productId;
    }

    public void SetAmount(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");
        Amount = amount;
    }

    public void SetWarehouseId(int warehouseId)
    {
        WarehouseId = warehouseId;
    }

    public void SetWarehouseName(string warehouseName)
    {
        if (string.IsNullOrWhiteSpace(warehouseName))
            throw new ArgumentException("O nome do depósito não pode ser nulo ou vazio.");
        WarehouseName = warehouseName.ToUpper();
    }

    public void DecreaseAmount(int amount)
    {
        if (Amount - amount < 0)
            throw new ProductWithoutStockException();
        Amount -= amount;
    }

    public void IncreaseAmount(int amount)
    {
        Amount += amount;
    }

    public void SetProduct(Product product)
    {
        Product = product;
    }
}