namespace Magnus.Core.Entities;

public class Bar : EntityBase
{
    public Guid ProductId { get; private set; }
    public Product? Product { get; private set; }
    public string Code { get; private set; } = ""; 

    private Bar()
    {
    }
    public Bar(Guid productId, string code, Product product)
    {
        SetProductId(productId);
        SetCode(code);
        SetProduct(product);
    }
    
    public void SetProductId(Guid productId)
    {
        if (productId == default)
        {
            throw new ArgumentException("O ProductId não pode ser um Guid vazio.");
        }
        ProductId = productId;
    }
    
    public void SetProduct(Product product)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product), "O produto não pode ser nulo.");
    }
    
    public void SetCode(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            throw new ArgumentNullException(nameof(code), "O código não pode ser nulo ou vazio.");
        }
        Code = code;
    }
}