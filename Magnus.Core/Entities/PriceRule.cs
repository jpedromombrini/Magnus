namespace Magnus.Core.Entities;

public class PriceRule : EntityBase
{
    public Guid ProductId { get; private set; } 
    public Product Product { get; private set; }
    public int From { get; private set; } 
    public decimal Price { get; private set; }
    public bool Active { get; private set; }

    private PriceRule(){ }

    public PriceRule(Guid productId, Product product, int from, decimal price, bool active)
    {
        SetProductId(productId);
        SetProduct(product);
        SetFrom(from);
        SetPrice(price);
        SetActive(active);
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("O preço deve ser maior que zero.");
        }

        Price = price;
    }

    public void SetActive(bool active)
    {
        Active = active;
    }

    public void SetFrom(int from)
    {
        if (from < 0)
        {
            throw new ArgumentException("A quantidade não pode ser negativa.");
        }

        From = from;
    }

    public void SetProductId(Guid productId)
    {
        if (productId == Guid.Empty)
        {
            throw new ArgumentException("O ProductId não pode ser um Guid vazio.");
        }

        ProductId = productId;
    }

    public void SetProduct(Product product)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product), "O produto não pode ser nulo.");
    }
}