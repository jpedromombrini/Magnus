namespace Magnus.Core.Entities;

public class CampaignItem : EntityBase
{
    public CampaignItem(Guid productId, decimal price)
    {
        SetProductId(productId);
        SetPrice(price);
    }

    private CampaignItem()
    {
    }

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public decimal Price { get; private set; }

    public void SetProductId(Guid productId)
    {
        ProductId = productId;
    }

    public void SetProduct(Product product)
    {
        Product = product;
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Informe um preço para o Produto");
        Price = price;
    }
}