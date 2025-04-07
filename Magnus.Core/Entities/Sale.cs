using Magnus.Core.Enumerators;

namespace Magnus.Core.Entities;

public class Sale : EntityBase
{
    public DateTime CreateAt { get; private set; }
    public int Document { get; private set; }
    public Guid ClientId { get; private set; }
    public string ClientName { get; private set; } = "";
    public Guid UserId { get; private set; }
    public decimal Value { get; private set; }
    public decimal FinantialDiscount { get; private set; }
    public SaleStatus Status { get; private set; }
    public List<SaleItem> Items { get; private set; }

    private Sale()
    {
    }

    public Sale(Guid clientId, string clientName, Guid userId, decimal value, decimal finantialDiscount,
        SaleStatus status)
    {
        SetCreateAt(DateTime.Now);
        SetClientId(clientId);
        SetClientName(clientName);
        SetUserId(userId);
        SetValue(value);
        SetFinantialDiscount(finantialDiscount);
        SetStatus(status);
    }

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
        ClientName = clientName;
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

    public void AddItem(SaleItem item)
    {
        Items ??= [];
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
        return Value - FinantialDiscount;
    }

    public decimal GetTotalItemValue()
    {
        return Items.Sum(x => x.TotalPrice - x.Discount);
    }
}