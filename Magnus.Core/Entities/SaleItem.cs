namespace Magnus.Core.Entities;

public class SaleItem : EntityBase
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Amount { get; private set; }
    public decimal Value { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal Discount { get; private set; }
    public DateOnly Validity { get; private set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }

    private SaleItem(){}
    
    public SaleItem(Guid productId, string productName, int amount, decimal value, decimal totalPrice, decimal discount, DateOnly validity)
    {
        SetProductId(productId);
        SetProductName(productName);
        SetAmount(amount);
        SetValue(value);
        SetTotalPrice(totalPrice);
        SetDiscount(discount);
        SetValidity(validity);
    }

    public void SetProductId(Guid productId)
    {
        if(productId == Guid.Empty)
            throw new ArgumentNullException("Informe o id do produto");
        ProductId = productId;
    }

    public void SetProductName(string productName)
    {
        if(string.IsNullOrEmpty(productName))
            throw new ArgumentNullException("Informe o nome do produto");
        ProductName = productName;
    }

    public void SetAmount(int amount)
    {
        if(amount <= 0)
            throw new ArgumentNullException("Informe a quantidade do produto");
        Amount = amount;
    }

    public void SetValue(decimal value)
    {
        if (value <= 0)
            throw new ArgumentNullException("Informe o valor");
        Value = value;
    }

    public void SetDiscount(decimal discount)
    {
        Discount = discount;
    }

    public void SetTotalPrice(decimal totalPrice)
    {
        if(totalPrice <=0)
            throw new ArgumentOutOfRangeException("Informe o Preço total");
        TotalPrice = totalPrice;
    }

    public void SetValidity(DateOnly validity)
    {
        Validity = validity;
    }
}