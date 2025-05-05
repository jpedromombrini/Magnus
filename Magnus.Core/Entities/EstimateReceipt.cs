namespace Magnus.Core.Entities;

public class EstimateReceipt : EntityBase
{
    public Guid? ClienteId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid EstimateId { get; private set; }
    public Guid ReceiptId { get; private set; }
    public Receipt Receipt { get; private set; }
    public ICollection<EstimateReceiptInstallment> Installments { get; private set; }

    private EstimateReceipt()
    {
    }

    public EstimateReceipt(Guid userId, Guid receiptId)
    {
        SetUserId(userId);
        SetReceiptId(receiptId);
    }
    public void SetEstimateId(Guid saleId)
    {
        if(saleId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id da venda");
        EstimateId = saleId;
    }
    public void SetClientId(Guid? clientId)
    {
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

    public void AddInstallments(IEnumerable<EstimateReceiptInstallment> installments)
    {
        Installments ??= [];
        foreach (var installment in installments)
            Installments.Add(installment);
    }
}