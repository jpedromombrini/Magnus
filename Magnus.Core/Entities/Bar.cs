namespace Magnus.Core.Entities;

public class Bar : EntityBase
{
    private Bar()
    {
    }

    public Bar(string code)
    {
        SetCode(code);
    }

    public Guid ProductId { get; private set; }
    public string Code { get; private set; }

    public void SetProductId(Guid productId)
    {
        if (productId == default) throw new ArgumentException("O ProductId não pode ser um Guid vazio.");
        ProductId = productId;
    }

    public void SetCode(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new ArgumentNullException(nameof(code), "O código não pode ser nulo ou vazio.");
        Code = code;
    }
}