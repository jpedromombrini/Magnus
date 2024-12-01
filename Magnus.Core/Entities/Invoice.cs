using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class Invoice : EntityBase
{
    public int Number { get; set; }
    public int Serie { get; set; }
    public string Key { get; set; }
    public DateTime DateEntry { get; set; }
    public DateTime Date { get; set; }
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; }
    public decimal Freight { get; set; }
    public decimal Deduction { get; set; }
    public decimal Value { get; set; }
    public string Observation { get; set; }
    public InvoiceSituation InvoiceSituation { get; set; }
    public List<InvoiceItem> Items { get; set; }
    public Guid? DoctorId { get; set; }

    public Invoice(int number, int serie, string key, DateTime dateEntry, DateTime date, Guid supplierId,
        string supplierName, decimal freight, decimal deduction, decimal value, string observation,
        InvoiceSituation invoiceSituation, Guid? doctorId)
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
        Items = [];
    }

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

    public void SetKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("A chave não pode ser nula ou vazia.");
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
        SupplierName = supplierName;
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

    public void SetItems(List<InvoiceItem> items)
    {
        if (items == null || items.Count == 0)
            throw new ArgumentException("A fatura deve ter pelo menos um item.");
        Items = items;
    }

    public void SetDoctorId(Guid? doctorId)
    {
        DoctorId = doctorId;
    }
}