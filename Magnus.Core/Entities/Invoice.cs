using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class Invoice : EntityBase
{
    private Invoice()
    {
    }

    public Invoice(
        int number,
        int serie,
        string? key,
        DateTime dateEntry,
        DateTime date,
        Guid supplierId,
        string supplierName,
        decimal freight,
        decimal deduction,
        decimal value,
        string observation,
        InvoiceSituation invoiceSituation,
        Guid? doctorId,
        bool updateFinantial,
        Guid? costCenterId,
        Guid laboratoryId)
    {
        SetNumber(number);
        SetSerie(serie);
        SetKey(key);
        SetDateEntry(dateEntry);
        SetDate(date);
        SetSupplierId(supplierId);
        SetSupplierName(supplierName);
        SetFreight(freight);
        SetDeduction(deduction);
        SetValue(value);
        SetObservation(observation);
        SetInvoiceSituation(invoiceSituation);
        SetDoctorId(doctorId);
        SetUpdateFinantial(updateFinantial);
        SetCostCenterId(costCenterId);
        SetLaboratoryId(laboratoryId);
        Items = [];
    }

    public int Number { get; private set; }
    public int Serie { get; private set; }
    public string? Key { get; private set; }
    public DateTime DateEntry { get; private set; }
    public DateTime Date { get; private set; }
    public Guid SupplierId { get; private set; }
    public string SupplierName { get; private set; }
    public decimal Freight { get; private set; }
    public decimal Deduction { get; private set; }
    public decimal Value { get; private set; }
    public string Observation { get; private set; }
    public InvoiceSituation InvoiceSituation { get; private set; }
    public ICollection<InvoiceItem> Items { get; private set; }
    public ICollection<InvoicePayment>? InvoicePayments { get; private set; }
    public Guid? DoctorId { get; private set; }
    public bool UpdateFinantial { get; private set; }
    public Guid? CostCenterId { get; private set; }
    public Guid LaboratoryId { get; private set; }

    public void SetNumber(int number)
    {
        if (number <= 0)
            throw new ArgumentException("O número da fatura deve ser positivo.");
        Number = number;
    }

    public void SetSerie(int serie)
    {
        if (serie <= 0)
            throw new ArgumentException("A série deve ser positiva.");
        Serie = serie;
    }

    public void SetKey(string? key)
    {
        Key = key;
    }

    public void SetDateEntry(DateTime dateEntry)
    {
        if (dateEntry > DateTime.Now)
            throw new ArgumentException("A data de entrada não pode ser no futuro.");
        DateEntry = dateEntry;
    }

    public void SetDate(DateTime date)
    {
        if (date > DateTime.Now)
            throw new ArgumentException("A data não pode ser no futuro.");
        Date = date;
    }

    public void SetSupplierId(Guid supplierId)
    {
        if (supplierId == Guid.Empty)
            throw new ArgumentException("O ID do fornecedor não pode ser vazio.");
        SupplierId = supplierId;
    }

    public void SetSupplierName(string supplierName)
    {
        if (string.IsNullOrWhiteSpace(supplierName))
            throw new ArgumentException("O nome do fornecedor não pode ser nulo ou vazio.");
        SupplierName = supplierName.ToUpper();
    }

    public void SetFreight(decimal freight)
    {
        if (freight < 0)
            throw new ArgumentException("O frete não pode ser negativo.");
        Freight = freight;
    }

    public void SetDeduction(decimal deduction)
    {
        if (deduction < 0)
            throw new ArgumentException("A dedução não pode ser negativa.");
        Deduction = deduction;
    }

    public void SetValue(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("O valor não pode ser negativo.");
        Value = value;
    }

    public void SetObservation(string observation)
    {
        Observation = observation ?? string.Empty;
    }

    public void SetInvoiceSituation(InvoiceSituation invoiceSituation)
    {
        InvoiceSituation = invoiceSituation;
    }

    public void SetItems(IEnumerable<InvoiceItem> items)
    {
        Items ??= [];
        foreach (var item in items)
            Items.Add(item);
    }

    public void SetDoctorId(Guid? doctorId)
    {
        DoctorId = doctorId;
    }

    public void AddRangePayments(IEnumerable<InvoicePayment> invoicePayments)
    {
        InvoicePayments ??= [];
        foreach (var invoicePayment in invoicePayments)
            InvoicePayments.Add(invoicePayment);
    }

    public void AddPayment(InvoicePayment invoicePayment)
    {
        InvoicePayments ??= [];
        InvoicePayments.Add(invoicePayment);
    }

    public decimal GetRealValue()
    {
        if (Value == 0m) return Value;
        return Value + Freight - Deduction;
    }

    public void SetUpdateFinantial(bool updateFinantial)
    {
        UpdateFinantial = updateFinantial;
    }

    public void SetCostCenterId(Guid? costCenterId)
    {
        CostCenterId = costCenterId;
    }

    public void SetLaboratoryId(Guid laboratoryId)
    {
        LaboratoryId = laboratoryId;
    }
}