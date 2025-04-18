namespace Magnus.Core.Entities;

public class AccountsReceipt : EntityBase
{
    public DateTime CreatedAt { get; private set; }
    public Guid? SaleInstallmentId { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateOnly? PaymentDate { get; private set; }
    public decimal PaymentValue { get; private set; }
    public decimal Value { get; private set; }
    public decimal Interest { get; private set; }
    public decimal Discount { get; private set; }
    public int Installment { get; private set; }

    private AccountsReceipt(){}
    public AccountsReceipt(
        Guid? saleInstallmentId,
        DateOnly dueDate,
        DateOnly? paymentDate,
        decimal paymentValue,
        decimal value,
        decimal interest,
        decimal discount,
        int installment)
    {
        CreatedAt = DateTime.Now;
        SaleInstallmentId = saleInstallmentId;
        DueDate = dueDate;
        PaymentDate = paymentDate;
        PaymentValue = paymentValue;
        Value = value;
        Interest = interest;
        Discount = discount;
        Installment = installment;
    }

    public void SetSaleInstallmentId(Guid? saleInstallmentId)
    {
        SaleInstallmentId = saleInstallmentId;
    }

    public void SetDueDate(DateOnly dueDate)
    {
        DueDate = dueDate;
    }

    public void SetPaymentDate(DateOnly paymentDate)
    {
        PaymentDate = paymentDate;
    }

    public void SetPaymentValue(decimal paymentValue)
    {
        if(paymentValue < 0)
            throw new ArgumentException("Pagamento não pode ser negativo.");
        PaymentValue = paymentValue;
    }

    public void SetValue(decimal value)
    {
        if(value < 0)
            throw new ArgumentException("Valor não pode ser negativo.");
        Value = value;
    }

    public void SetInterest(decimal interest)
    {
        if(interest < 0)
            throw new ArgumentException("Juros não pode ser negativo.");
        Interest = interest;
    }

    public void SetDiscount(decimal discount)
    {
        if(discount < 0)
            throw new ArgumentException("Desconto não pode ser negativo.");
        Discount = discount;
    }

    public void SetInstallment(int installment)
    {
        if(installment <= 0)
            throw new ArgumentException("Parcela inválida");
        Installment = installment;
    }
    
}