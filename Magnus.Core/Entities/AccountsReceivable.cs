using Magnus.Core.Enumerators;
using Magnus.Core.Helpers;

namespace Magnus.Core.Entities;

public class AccountsReceivable : EntityBase
{
    private AccountsReceivable()
    {
    }

    public AccountsReceivable(
        Guid clientId,
        Guid? saleReceiptInstallmentId,
        int document,
        DateOnly dueDate,
        decimal value,
        decimal interest,
        decimal discount,
        int installment,
        int totalInstallment,
        Guid costCenterId)
    {
        CreatedAt = DateTimeHelper.NowInBrasilia();
        SetSaleReceiptInstallmentId(saleReceiptInstallmentId);
        SetClientId(clientId);
        SetDocument(document);
        SetDueDate(dueDate);
        SetValue(value);
        SetInterest(interest);
        SetDiscount(discount);
        SetInstallment(installment);
        SetTotalInstallment(totalInstallment);
        SetCostCenterId(costCenterId);
    }

    public DateTime CreatedAt { get; private set; }
    public Guid? SaleReceiptInstallmentId { get; private set; }
    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }
    public int Document { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateTime? ReceiptDate { get; private set; }
    public decimal ReceiptValue { get; private set; }
    public Guid? ReceiptId { get; private set; }
    public Receipt? Receipt { get; private set; }
    public decimal Value { get; private set; }
    public decimal Interest { get; private set; }
    public decimal Discount { get; private set; }
    public int Installment { get; private set; }
    public Guid? CostCenterId { get; private set; }
    public CostCenter? CostCenter { get; private set; }
    public string? Observation { get; private set; }
    public byte[]? ProofImage { get; private set; }
    public int TotalInstallment { get; private set; }
    public AccountsReceivableStatus Status { get; private set; }
    public Guid? RootAccontsReceivableId { get; private set; }
    public ICollection<AccountsReceivableOccurence>? AccountsReceivableOccurences { get; private set; }

    public void SetSaleReceiptInstallmentId(Guid? saleReceiptInstallmentId)
    {
        SaleReceiptInstallmentId = saleReceiptInstallmentId;
    }

    public void SetClientId(Guid clientId)
    {
        if (clientId == Guid.Empty)
            throw new ArgumentNullException("Informe o id do cliente");
        ClientId = clientId;
    }

    public void ReversePayment()
    {
        ReceiptDate = null;
        ReceiptValue = 0;
        Status = AccountsReceivableStatus.Open;
    }

    public void SetClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        Client = client;
    }

    public void SetReceipt(Receipt receipt)
    {
        ArgumentNullException.ThrowIfNull(receipt);
        Receipt = receipt;
    }

    public void SetReceiptId(Guid receiptId)
    {
        if (receiptId == Guid.Empty)
            throw new ArgumentNullException("Informe o id do recebimento");
    }

    public void SetDocument(int document)
    {
        Document = document;
    }

    public void SetDueDate(DateOnly dueDate)
    {
        DueDate = dueDate;
    }

    public void SetReceiptDate(DateTime? receiptDate)
    {
        ReceiptDate = receiptDate;
    }

    public void SetReceiptValue(decimal receiptValue)
    {
        if (receiptValue < 0)
            throw new ArgumentException("Pagamento não pode ser negativo.");
        ReceiptValue = receiptValue;
    }

    public void SetValue(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Valor não pode ser negativo.");
        Value = value;
    }

    public void SetInterest(decimal interest)
    {
        if (interest < 0)
            throw new ArgumentException("Juros não pode ser negativo.");
        Interest = interest;
    }

    public void SetDiscount(decimal discount)
    {
        if (discount < 0)
            throw new ArgumentException("Desconto não pode ser negativo.");
        Discount = discount;
    }

    public void SetInstallment(int installment)
    {
        if (installment <= 0)
            throw new ArgumentException("Parcela inválida");
        Installment = installment;
    }

    public void SetCostCenterId(Guid costCenterId)
    {
        if (costCenterId == Guid.Empty)
            throw new ArgumentNullException("Informe o id do centro de custo");
        CostCenterId = costCenterId;
    }

    public void SetCostCenter(CostCenter costCenter)
    {
        if (costCenter == null)
            throw new ArgumentNullException("Informe o centro de custo");
        CostCenter = costCenter;
    }

    public void SetObservation(string observation)
    {
        if (string.IsNullOrEmpty(observation))
            throw new ArgumentNullException("Informe uma observação válida");
        Observation = observation;
    }

    public void SetStatus(AccountsReceivableStatus status)
    {
        Status = status;
    }

    public void SetProofImage(string? proofImageBase64)
    {
        if (string.IsNullOrEmpty(proofImageBase64)) return;
        var base64Data = proofImageBase64[(proofImageBase64.IndexOf(',') + 1)..];
        var proofImage = Convert.FromBase64String(base64Data);
        ProofImage = proofImage;
    }

    public void SetTotalInstallment(int totalInstallment)
    {
        if (totalInstallment <= 0)
            throw new ArgumentException("Valor não pode ser negativo.");
        TotalInstallment = totalInstallment;
    }

    public string? GetProofImageBase64()
    {
        return ProofImage == null ? null : $"data:image/jpeg;base64,{Convert.ToBase64String(ProofImage)}";
    }

    public void SetRootAccontsReceivableId(Guid? rootAccontsReceivableId)
    {
        RootAccontsReceivableId = rootAccontsReceivableId;
    }

    public void AddOcurrence(AccountsReceivableOccurence occurrence)
    {
        AccountsReceivableOccurences ??= [];
        AccountsReceivableOccurences.Add(occurrence);
    }
}