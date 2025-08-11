namespace Magnus.Core.Entities;

public class InvoiceItem : EntityBase
{
    private InvoiceItem()
    {
    }

    public InvoiceItem(Guid productId, int productInternalCode, string productName, int amount, decimal totalValue,
        DateOnly validate, string lot)
    {
        SetProductId(productId);
        SetProductInternalCode(productInternalCode);
        SetProductName(productName);
        SetAmount(amount);
        SetTotalValue(totalValue);
        SetValidate(validate);
        SetLot(lot);
    }

    public Guid ProductId { get; private set; }
    public int ProductInternalCode { get; private set; }
    public string ProductName { get; private set; }
    public int Amount { get; private set; }
    public decimal TotalValue { get; private set; }
    public Guid InvoiceId { get; private set; }
    public DateOnly Validate { get; private set; }
    public string Lot { get; private set; }
    public Invoice Invoice { get; private set; }

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
        ProductName = productName.ToUpper();
    }

    public void SetAmount(int amount)
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

    public void SetInvoice(Invoice invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        if (invoice.Id == Guid.Empty)
            throw new ArgumentException("O ID do pedido não pode ser um GUID vazio.");
        Invoice = invoice;
        InvoiceId = invoice.Id;
    }

    public void SetValidate(DateOnly validate)
    {
        Validate = validate;
    }

    public void SetLot(string lot)
    {
        Lot = lot;
    }
}