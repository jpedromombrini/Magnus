namespace Magnus.Core.Entities;

public class SaleReceipt : EntityBase
{
    public Guid ClienteId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid SaleId { get; private set; }
    public Guid ReceiptId { get; private set; }
    public Receipt Receipt { get; private set; }
    public List<SaleReceiptInstallment> Installments { get; private set; }

    private SaleReceipt() {}
    public SaleReceipt(Guid clienteId, Guid userId, Guid saleId, Guid receiptId)
    {
        SetClientId(clienteId);
        SetUserId(userId);
        SetSaleId(saleId);
        SetReceiptId(receiptId);
    }

    public void SetSaleId(Guid saleId)
    {
        if(saleId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id da venda");
        SaleId = saleId;
    }
    public void SetClientId(Guid clientId)
    {
        if(clientId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do cliente");
        ClienteId = clientId;
    }
    public void SetUserId(Guid userId)
    {
        if(userId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do Usuário");
        UserId = userId;
    }
    public void SetReceiptId(Guid receiptId)
    {
        if(receiptId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do Recebimento");
        ReceiptId = receiptId;
    }

    public void SetReceipt(Receipt receipt)
    {
        ArgumentNullException.ThrowIfNull(receipt);
        Receipt = receipt;
    }

    public void AddInstallment(SaleReceiptInstallment installment)
    {
        Installments ??= [];
        Installments.Add(installment);
    }
}