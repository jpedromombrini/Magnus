namespace Magnus.Core.Entities;

public class EstimateItem : EntityBase
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Amount { get; private set; }
    public decimal TotalValue { get; private set; }
    public decimal Value { get; private set; }
    public decimal Discount { get; private set; }
    public Guid EstimateId { get; private set; }
    public Estimate Estimate { get; private set; }

    private EstimateItem(){}
    public EstimateItem(Guid productId, string productName, int amount, decimal totalValue, decimal discount, Estimate estimate)
    {
        SetProductId(productId);
        SetProductName(productName);
        SetAmount(amount);
        SetTotalValue(totalValue);
        SetDiscount(discount);
        SetEstimate(estimate);
    }

    public void SetProductId(Guid productId)
    {
        if(productId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do produto");
        ProductId = productId;
    }

    public void SetProductName(string productName)
    {
        if(string.IsNullOrWhiteSpace(productName))
            throw new ArgumentNullException("Informe o nome do produto");
        ProductName = productName;
    }

    public void SetAmount(int amount)
    {
        if(amount <= 0)
            throw new ArgumentNullException("Informe a quantidade do produto");
        Amount = amount;
    }

    public void SetTotalValue(decimal totalValue)
    {
        if (totalValue <= 0m)
            throw new ArgumentNullException("Informe o valor total do produto");
        TotalValue = totalValue;
    }

    public void setValue(decimal value)
    {
        if(value <= 0m)
            throw new ArgumentNullException("Informe o valor do produto");
        Value = value;
    }
    
    public void SetDiscount(decimal discount)
    {
        if (discount < 0)
            throw new ArgumentNullException("Informe um disconto maior ou igual a 0");
        Discount = discount;
    }

    public void SetEstimate(Estimate estimate)
    {
        ArgumentNullException.ThrowIfNull(estimate);
        if (estimate.Id == Guid.Empty)
            throw new ArgumentException("O ID do Orçamento não pode ser um GUID vazio.");
        Estimate = estimate;
        EstimateId = estimate.Id;
    }
    
}