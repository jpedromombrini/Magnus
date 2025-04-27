namespace Magnus.Core.Entities;

public class SaleReceiptInstallment : EntityBase
{
    public Guid SaleReceiptId { get; private set; }
    public SaleReceipt SaleReceipt { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public decimal PaymentValue { get; private set; }
    public decimal Value { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Interest { get; private set; }
    public int Installment { get; private set; }
    public byte[]? ProofImage { get; private set; }

    private SaleReceiptInstallment()
    {
    }

    public SaleReceiptInstallment(Guid saleReceiptId, DateOnly dueDate, DateTime? paymentDate, decimal paymentValue,
        decimal value,
        decimal discount, decimal interest, int installment, string? proofImageBase64)
    {
        SetSaleReceiptId(saleReceiptId);
        SetDueDate(dueDate);
        SetPaymentDate(paymentDate);
        SetPaymentValue(paymentValue);
        SetValue(value);
        SetDiscount(discount);
        SetInterest(interest);
        SetInstallment(installment);
        SetProofImage(proofImageBase64);
    }

    public void SetSaleReceiptId(Guid saleReceiptId)
    {
        if (saleReceiptId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do recebimento");
        SaleReceiptId = saleReceiptId;
    }

    public void SetSaleReceipt(SaleReceipt saleReceipt)
    {
        if (saleReceipt.Id == Guid.Empty)
            throw new ArgumentException("Informe um id de venda pagamento");
        SaleReceiptId = saleReceipt.Id;
        SaleReceipt = saleReceipt;
    }

    public void SetDueDate(DateOnly dueDate)
    {
        DueDate = dueDate;
    }

    public void SetPaymentDate(DateTime? paymentDate)
    {
        PaymentDate = paymentDate;
    }

    public void SetPaymentValue(decimal paymentValue)
    {
        PaymentValue = paymentValue;
    }

    public void SetValue(decimal value)
    {
        if (value <= 0m)
            throw new ArgumentException("Informe um valor");
        Value = value;
    }

    public void SetDiscount(decimal discount)
    {
        if (discount < 0m)
            throw new ArgumentException("Informe o desconto");
        Discount = discount;
    }

    public void SetInterest(decimal interest)
    {
        if (interest < 0m)
            throw new ArgumentException("Informe o juros");
        Interest = interest;
    }

    public void SetInstallment(int installment)
    {
        if (installment <= 0)
            throw new ArgumentException("Informe uma parcela");
        Installment = installment;
    }

    public decimal GetRealValue()
    {
        if (Value == 0m) return Value;
        return Value + Interest - Discount;
    }

    public void SetProofImage(string? proofImageBase64)
    {
        if (string.IsNullOrEmpty(proofImageBase64)) return;
        var base64Data = proofImageBase64[(proofImageBase64.IndexOf(',') + 1)..];
        var proofImage = Convert.FromBase64String(base64Data);
        ProofImage = proofImage;
    }

    public string? GetProofImageBase64()
    {
        return ProofImage == null ? null : $"data:image/jpeg;base64,{Convert.ToBase64String(ProofImage)}";
    }
}