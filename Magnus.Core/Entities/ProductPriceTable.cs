namespace Magnus.Core.Entities;

public class ProductPriceTable : EntityBase
{
    public Guid ProductId { get; private set; }
    public int MinimalAmount { get; private set; }
    public int MaximumAmount { get; private set; }
    public decimal Price { get; private set; }

    private ProductPriceTable(){}
    public ProductPriceTable(Guid productId, int minimalAmount, int maximumAmount, decimal price)
    {
        SetProductId(productId);
        SetMinimalAmount(minimalAmount);
        SetMaximumAmount(maximumAmount);
        SetPrice(price);
    }
    public void SetProductId(Guid productId)
    {
        if(productId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do Produto");
        ProductId = productId;
    }

    public void SetMinimalAmount(int minimalAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(minimalAmount);
        MinimalAmount = minimalAmount;
    }

    public void SetMaximumAmount(int maximumAmount)
    {
        MaximumAmount = maximumAmount;
    }

    public void SetPrice(decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(price);
        Price = price;
    }
}