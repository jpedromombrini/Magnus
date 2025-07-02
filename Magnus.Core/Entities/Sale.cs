using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class Sale : EntityBase
{
    private Sale()
    {
    }

    public Sale(Guid clientId, Guid userId, decimal value, decimal freight, decimal finantialDiscount)
    {
        SetCreateAt(DateTime.Now);
        SetClientId(clientId);
        SetUserId(userId);
        SetValue(value);
        SetFreight(freight);
        SetFinantialDiscount(finantialDiscount);
    }

    public DateTime CreateAt { get; private set; }
    public int Document { get; private set; }
    public Guid ClientId { get; private set; }
    public string ClientName { get; private set; } = "";
    public Guid UserId { get; private set; }
    public decimal Value { get; private set; }
    public Guid FreightId { get; private set; }
    public decimal Freight { get; private set; }
    public decimal FinantialDiscount { get; private set; }
    public SaleStatus Status { get; private set; }
    public ICollection<SaleItem> Items { get; private set; }
    public ICollection<SaleReceipt>? Receipts { get; private set; }
    public Guid? EstimateId { get; private set; }
    public string ReasonCancel { get; private set; }

    public void SetCreateAt(DateTime createAt)
    {
        CreateAt = createAt;
    }

    public void SetClientId(Guid clientId)
    {
        if (clientId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do cliente");
        ClientId = clientId;
    }

    public void SetClientName(string clientName)
    {
        if (string.IsNullOrWhiteSpace(clientName))
            throw new ArgumentException("Informe o Nome do cliente");
        ClientName = clientName.ToUpper();
    }

    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do usuário");
        UserId = userId;
    }

    public void SetValue(decimal value)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
        Value = value;
    }

    public void SetFreightId(Guid freightId)
    {
        FreightId = freightId;
    }

    public void SetFreight(decimal freight)
    {
        Freight = freight;
    }

    public void AddItem(SaleItem item)
    {
        Items ??= [];
        Items.Add(item);
    }

    public void AddItems(IEnumerable<SaleItem> items)
    {
        Items ??= [];
        foreach (var item in items)
            Items.Add(item);
    }

    public void RemoveItem(SaleItem item)
    {
        Items.Remove(item);
    }

    public void SetStatus(SaleStatus status)
    {
        Status = status;
    }

    public void SetFinantialDiscount(decimal finantialDiscount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(finantialDiscount);
        FinantialDiscount = finantialDiscount;
    }

    public decimal GetRealValue()
    {
        if (Value == 0m) return Value;
        return Value + Freight - FinantialDiscount;
    }

    public void SetReasonCancel(string reason)
    {
        ReasonCancel = reason;
    }

    public decimal GetTotalItemValue()
    {
        return Items.Sum(x => x.TotalPrice - x.Discount);
    }

    public void SetEstimateId(Guid estimateId)
    {
        EstimateId = estimateId;
    }

    public void AddRangeReceipts(IEnumerable<SaleReceipt> saleReceipts)
    {
        Receipts ??= [];
        foreach (var saleReceipt in saleReceipts)
            Receipts.Add(saleReceipt);
    }
}