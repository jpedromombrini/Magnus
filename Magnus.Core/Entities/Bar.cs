namespace Magnus.Core.Entities;

public class Bar : EntityBase
{
    public Guid ProductId { get; private set; }
    public string Code { get; private set; } = ""; 

    private Bar()
    {
    }
    public Bar(Guid productId, string code)
    {
        SetProductId(productId);
        SetCode(code);
    }
    
    public void SetProductId(Guid productId)
    {
        if (productId == default)
        {
            throw new ArgumentException("O ProductId não pode ser um Guid vazio.");
        }
        ProductId = productId;
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