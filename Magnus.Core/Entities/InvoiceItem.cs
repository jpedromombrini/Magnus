namespace Magnus.Core.Entities;

public class InvoiceItem : EntityBase
{
    public Guid ProductId { get; private  set; }
    public int ProductInternalCode { get; private set; }
    public string ProductName { get; private set; } 
    public decimal Amount { get; private set; }
    public decimal TotalValue { get; private set; }
    public DateOnly Validity { get; private set; }
    public bool Bonus { get; private set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    private InvoiceItem(){}
    public InvoiceItem(Guid productId, int productInternalCode, string productName, decimal amount, decimal totalValue,
        DateOnly validity, bool bonus)
    {
        SetProductId(productId);
        SetProductInternalCode(productInternalCode);
        SetProductName(productName);
        SetAmount(amount);
        SetTotalValue(totalValue);
        SetValidity(validity);
        SetBonus(bonus);
    }
    public void SetProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("O ID do produto não pode ser um GUID vazio.");
        ProductId = productId;
    }

    public void SetProductInternalCode(int productInternalCode)
    {
        if (productInternalCode <= 0)
            throw new ArgumentException("O código interno do produto deve ser um valor positivo.");
        ProductInternalCode = productInternalCode;
    }

    public void SetProductName(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("O nome do produto não pode ser nulo ou vazio.");
        ProductName = productName;
    }

    public void SetAmount(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("A quantidade não pode ser negativa.");
        Amount = amount;
    }

    public void SetTotalValue(decimal totalValue)
    {
        if (totalValue < 0)
            throw new ArgumentException("O valor total não pode ser negativo.");
        TotalValue = totalValue;
    }

    public void SetValidity(DateOnly validity)
    {
        if (validity > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("A validade não pode ser uma data no futuro.");
        Validity = validity;
    }

    public void SetBonus(bool bonus)
    {
        Bonus = bonus;
    }

    public void SetInvoice(Invoice invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        if (invoice.Id == Guid.Empty)
            throw new ArgumentException("O ID do pedido não pode ser um GUID vazio.");
        Invoice = invoice;
        InvoiceId = invoice.Id;
    }
}