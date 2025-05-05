namespace Magnus.Core.Entities;

public class EstimateReceiptInstallment : EntityBase
{
    public Guid EstimateReceiptId { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public decimal PaymentValue { get; private set; }
    public decimal Value { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Interest { get; private set; }
    public int Installment { get; private set; }

    private EstimateReceiptInstallment()
    {
    }

    public EstimateReceiptInstallment(DateOnly dueDate, DateTime? paymentDate, decimal paymentValue,
        decimal value,
        decimal discount, decimal interest, int installment)
    {
        SetDueDate(dueDate);
        SetPaymentDate(paymentDate);
        SetPaymentValue(paymentValue);
        SetValue(value);
        SetDiscount(discount);
        SetInterest(interest);
        SetInstallment(installment);
    }

    public void SetEstimateReceiptId(Guid estimateReceiptId)
    {
        if (estimateReceiptId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do recebimento");
        EstimateReceiptId = estimateReceiptId;
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
}