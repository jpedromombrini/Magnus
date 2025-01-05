using Magnus.Core.Exceptions;

namespace Magnus.Core.Entities;

public class ProductStock : EntityBase
{
    public Guid ProductId { get; set; }
    public DateOnly ValidityDate { get; set; }
    public decimal Amount { get; set; }
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; } 
    public ProductStock(Guid productId, DateOnly validityDate, decimal amount, int warehouseId, string warehouseName)
    {
        SetProductId(productId);
        SetValidityDate(validityDate);
        SetAmount(amount);
        SetWarehouseId(warehouseId);
        SetWarehouseName(warehouseName);
    }
    
    public void SetProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("O ProductId não pode ser vazio.");
        ProductId = productId;
    }

    public void SetValidityDate(DateOnly validityDate)
    {
        if (validityDate == default)
            throw new ArgumentException("A data de validade não pode ser nula.");
        ValidityDate = validityDate;
    }

    public void SetAmount(decimal amount)
    {
        if (amount <= 0)
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
        WarehouseName = warehouseName;
    }

    public void DecreaseAmount(decimal amount)
    {
        if(Amount - amount < 0)
            throw new ProductWithoutStockException();
        Amount -= amount;
    }

    public void IncreaseAmount(decimal amount)
    {
        Amount += amount;
    }
}