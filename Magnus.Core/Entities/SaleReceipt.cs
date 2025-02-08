namespace Magnus.Core.Entities;

public class SaleReceipt : EntityBase
{
    public Guid SaleId { get; private set; }
    public Sale Sale { get; private set; }
    public Guid ReceiptId { get; private set; }
    public Receipt Receipt { get; private set; }
    public List<SaleReceiptInstallment> Installments { get; private set; }

    private SaleReceipt() {}
    public SaleReceipt(Sale sale, Receipt receipt)
    {
        SetSale(sale);
        SetReceipt(receipt);
    }

    public void SetSale(Sale sale)
    {
        if(sale.Id == Guid.Empty)
            throw new ArgumentNullException("Informe o Id da venda");
        SaleId = sale.Id;
        Sale = sale;
    }

    public void SetReceipt(Receipt receipt)
    {
        if (receipt.Id == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do recebimento");
        ReceiptId = receipt.Id;
        Receipt = receipt;
    }

    public void AddInstallment(SaleReceiptInstallment installment)
    {
        Installments ??= [];
        Installments.Add(installment);
    }
}