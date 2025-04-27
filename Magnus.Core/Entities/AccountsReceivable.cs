using System.ComponentModel;
using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class AccountsReceivable : EntityBase
{
    public DateTime CreatedAt { get; private set; }
    public Guid? SaleReceiptInstallmentId { get; private set; }
    public Guid ClientId { get; private set; }
    public string ClientName { get; private set; }
    public int Document  { get; private set; }
    public DateOnly DueDate { get; private set; }
    public DateOnly? PaymentDate { get; private set; }
    public decimal PaymentValue { get; private set; }
    public decimal Value { get; private set; }
    public decimal Interest { get; private set; }
    public decimal Discount { get; private set; }
    public int Installment { get; private set; }
    public string CostCenter { get; private set; }
    public string? Observation { get; private set; }
    public AccountsReceivableStatus Status { get; private set; }

    private AccountsReceivable(){}
    public AccountsReceivable(
        Guid? saleReceiptInstallmentId,
        Guid clientId,
        string clientName,
        int document,
        DateOnly dueDate,
        DateOnly? paymentDate,
        decimal paymentValue,
        decimal value,
        decimal interest,
        decimal discount,
        int installment,
        string costCenter)
    {
        CreatedAt = DateTime.Now;
        SetSaleReceiptInstallmentId(saleReceiptInstallmentId);
        SetClientId(clientId);
        SetClientName(clientName);
        SetDocument(document);
        SetDueDate(dueDate);
        SetPaymentDate(paymentDate);
        SetPaymentValue(paymentValue);
        SetValue(value);
        SetInterest(interest);
        SetDiscount(discount);
        SetInstallment(installment);
        SetCostCenter(costCenter);
    }

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

    public void SetClientName(string clientName)
    {
        if(string.IsNullOrEmpty(clientName))
            throw new ArgumentNullException("Informe o nome do cliente");
        ClientName = clientName;
    }

    public void SetDocument(int document)
    {
        Document = document;
    }
    public void SetDueDate(DateOnly dueDate)
    {
        DueDate = dueDate;
    }

    public void SetPaymentDate(DateOnly? paymentDate)
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

    public void SetCostCenter(string costCenter)
    {
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
}