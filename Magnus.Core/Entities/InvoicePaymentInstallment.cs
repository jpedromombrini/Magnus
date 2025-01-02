namespace Magnus.Core.Entities;

public class InvoicePaymentInstallment : EntityBase
{
    public Guid InvoicePaymentId { get; private set; }
    public InvoicePayment InvoicePayment { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public decimal Value { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Interest { get; private set; }
    public int Installment { get; private set; }

    private InvoicePaymentInstallment(){}
    public InvoicePaymentInstallment(InvoicePayment invoicePayment, DateOnly dueDate, DateTime? paymentDate,
        decimal value, decimal discount, decimal interest, int installment)
    {
        SetInvoicePayment(invoicePayment);
        SetDueDate(dueDate);
        SetValue(value);
        SetValue(discount);
        SetValue(interest);
        SetValue(installment);
    }

    public void SetDueDate(DateOnly dueDate)
    {
        DueDate = dueDate;
    }

    public void SetPaymentDate(DateTime paymentDate)
    {
        PaymentDate = paymentDate;
    }

    public void SetValue(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("Informe um valor maior qur zero");
        Value = value;
    }

    public void SetDiscount(decimal discount)
    {
        Discount = discount;
    }

    public void SetInstallment(int installment)
    {
        if (installment < 0)
            throw new ArgumentException("Informe um valor maior que zero para parcela");
        Installment = installment;
    }

    public void SetInterest(decimal interest)
    {
        Interest = interest;
    }

    public void SetInvoicePayment(InvoicePayment invoicePayment)
    {
        if(invoicePayment  is null)
            throw new ArgumentNullException("Informe um pagamento");
        if(invoicePayment.Id == Guid.Empty)
            throw new ArgumentNullException("Informe um pagamento");
        InvoicePaymentId = invoicePayment.Id;
        InvoicePayment = invoicePayment;
    }
    
    
}