namespace Magnus.Core.Entities;

public class InvoicePayment : EntityBase
{
    public Guid SupplierId { get; private set; }
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; }
    public Guid PaymentId { get; private set; }
    public Payment Payment { get; private set; }
    public List<InvoicePaymentInstallment> Installments { get; private set; }

    private InvoicePayment(){}
    
    public InvoicePayment(Guid paymentId, Guid supplierId)
    {
        SetPaymentId(paymentId);
        SetSupplierId(supplierId);
    }

    public void SetInvoice(Invoice invoice)
    {
        if(invoice is null)
            throw new ArgumentNullException("Informe a nota");
        InvoiceId = invoice.Id;
        Invoice = invoice;
    }
    
    public void SetPayment(Payment payment)
    {
        ArgumentNullException.ThrowIfNull(payment);
        Payment = payment;
    }
    public void SetPaymentId(Guid paymentId)
    {
        PaymentId = paymentId;
    }
    public void SetSupplierId(Guid supplierId)
    {
        SupplierId = supplierId;
    }

    public void AddInstallment(InvoicePaymentInstallment installment)
    {
        Installments ??= [];
        Installments.Add(installment);
    }
}