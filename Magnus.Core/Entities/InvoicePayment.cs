namespace Magnus.Core.Entities;

public class InvoicePayment : EntityBase
{
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; }
    public Guid PaymentId { get; private set; }
    public Payment Payment { get; private set; }
    public List<InvoicePaymentInstallment> Installments { get; private set; }

    private InvoicePayment(){}
    
    public InvoicePayment(Invoice invoice, Payment payment)
    {
        SetInvoice(invoice);
        SetPayment(payment);
    }

    public void SetInvoice(Invoice invoice)
    {
        if(invoice is null)
            throw new ArgumentNullException("Informe a nota");
        if(invoice.Id == Guid.Empty)
            throw new ArgumentNullException("Informe o Id da nota");
        InvoiceId = invoice.Id;
        Invoice = invoice;
    }
    
    public void SetPayment(Payment payment)
    {
        if(payment is null)
            throw new ArgumentNullException("Informe o pagamento da nota");
        if(payment.Id == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do pagendamento da nota");
        PaymentId = payment.Id;
        Payment = payment;
    }

    public void AddInstallment(InvoicePaymentInstallment installment)
    {
        Installments ??= [];
        Installments.Add(installment);
    }
}