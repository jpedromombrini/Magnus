namespace Magnus.Core.Entities;

public class AccountsPayable : EntityBase
{    
    public int Document { get; private set; }
    public Guid SupplierId { get; private set; }
    public string SupplierName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }
    public decimal Value { get; private set; }
    public decimal PaymentValue { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Interest { get; private set; }
    public Guid CostCenterId { get; private set; }
    public int Installment { get; private set; }
    public Guid? InvoiceId { get; private set; }
    public Guid? UserPaymentId { get; private set; }
    public bool Canceled { get; private set; }
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; }
    public List<AccountsPayableOccurrence>? Occurrences { get; private set; }
    private AccountsPayable(){}
    
     public AccountsPayable(
        int document,
        Guid supplierId,
        string supplierName,
        DateTime createdAt,
        DateOnly dueDate,
        DateTime? paymentDate,
        decimal value,
        decimal paymentValue,
        decimal discount,
        decimal interest,
        Guid costCenterId,
        int installment,
        Guid? invoiceId,
        Guid? userPaymentId,
        bool canceled,
        Payment payment)
    {
        SetDocument(document);
        SetSupplierId(supplierId);
        SetSupplierName(supplierName);
        SetCreatedAt(createdAt);
        SetDueDate(dueDate);
        SetPaymentDate(paymentDate);
        SetValue(value);
        SetPaymentValue(paymentValue);
        SetDiscount(discount);
        SetInterest(interest);
        SetCostCenter(costCenterId);
        SetInstallment(installment);
        SetInvoiceId(invoiceId);
        SetUserPaymentId(userPaymentId);
        SetCanceled(canceled);
        SetPayent(payment);
    }

   public void SetDocument(int document)
    {
        if (document <= 0)
            throw new ArgumentOutOfRangeException(nameof(document), "O número do documento deve ser maior que zero.");
        Document = document;
    }

    public void SetSupplierId(Guid supplierId)
    {
        if (supplierId == Guid.Empty)
            throw new ArgumentNullException(nameof(supplierId), "O ID do fornecedor não pode ser vazio.");
        SupplierId = supplierId;
    }

    public void SetSupplierName(string supplierName)
    {
        if (string.IsNullOrEmpty(supplierName))
            throw new ArgumentNullException(nameof(supplierName), "O nome do fornecedor não pode ser nulo ou vazio.");
        SupplierName = supplierName;
    }

    public void SetCreatedAt(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    public void SetDueDate(DateOnly dueDate)
    {
        DueDate = dueDate;
    }

    public void SetPaymentDate(DateTime? paymentDate)
    {
        PaymentDate = paymentDate;
    }

    public void SetValue(decimal value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value), "O valor deve ser maior que zero.");
        Value = value;
    }

    public void SetPaymentValue(decimal paymentValue)
    {
        if (paymentValue < 0)
            throw new ArgumentOutOfRangeException(nameof(paymentValue), "O valor do pagamento não pode ser negativo.");
        PaymentValue = paymentValue;
    }

    public void SetDiscount(decimal discount)
    {
        if (discount < 0)
            throw new ArgumentOutOfRangeException(nameof(discount), "O desconto não pode ser negativo.");
        Discount = discount;
    }

    public void SetInterest(decimal interest)
    {
        if (interest < 0)
            throw new ArgumentOutOfRangeException(nameof(interest), "Os juros não podem ser negativos.");
        Interest = interest;
    }

    public void SetCostCenter(Guid costCenterId)
    {
        CostCenterId = costCenterId;
    }

    public void SetInstallment(int installment)
    {
        if (installment <= 0)
            throw new ArgumentOutOfRangeException(nameof(installment), "A parcela deve ser maior que zero.");
        Installment = installment;
    }

    public void SetInvoiceId(Guid? invoiceId)
    {
        InvoiceId = invoiceId;
    }

    public void SetUserPaymentId(Guid? userPaymentId)
    {
        if (userPaymentId == Guid.Empty)
            throw new ArgumentNullException(nameof(userPaymentId), "O ID do pagamento do usuário não pode ser vazio.");
        UserPaymentId = userPaymentId;
    }

    public void SetCanceled(bool canceled)
    {
        Canceled = canceled;
    }

    public void SetPayent(Payment payment)
    {
        if(payment is null)
            throw new ArgumentNullException("Informe o pagamento");
        if(payment.Id == Guid.Empty)
            throw new ArgumentException("informe o Id do pagamento");
        PaymentId = payment.Id;
        Payment = payment;
        
    }

    public void SetOccurrences(List<AccountsPayableOccurrence>? occurrences)
    {
        Occurrences ??= [];
        Occurrences = occurrences ?? [];
    }

    public void AddOccurrence(AccountsPayableOccurrence occurrence)
    {
        if (occurrence is null)
            throw new ArgumentException("Ocorrência não pode ser nula");
        Occurrences ??= [];
        Occurrences.Add(occurrence);
    }
}