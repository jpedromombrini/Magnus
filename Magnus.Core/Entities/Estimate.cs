using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;

namespace Magnus.Core.Entities;

public class Estimate : EntityBase
{
    private Estimate()
    {
    }

    public Estimate(string? description, DateTime validitAt, Guid? clientId, string? clientName, decimal value,
        decimal freight, decimal finantialDiscount, Guid userId)
    {
        SetCreatedAt(DateTime.Now);
        SetDescription(description);
        SetValidity(validitAt);
        SetClientId(clientId);
        SetClientName(clientName);
        SetValue(value);
        SetFreight(freight);
        SetFinantialDiscount(finantialDiscount);
        SetUserId(userId);
    }

    public int Code { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ValiditAt { get; private set; }
    public Guid? ClientId { get; private set; }
    public string? ClientName { get; private set; }
    public decimal Value { get; private set; }
    public Guid? FreightId { get; private set; }
    public decimal Freight { get; private set; }
    public decimal FinantialDiscount { get; private set; }
    public string? Observation { get; set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public ICollection<EstimateItem> Items { get; private set; }
    public ICollection<EstimateReceipt>? Receipts { get; private set; }
    public EstimateStatus EstimateStatus { get; private set; }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetValidity(DateTime validitAt)
    {
        ValiditAt = validitAt;
    }

    public void SetClientId(Guid? clientId)
    {
        ClientId = clientId;
    }

    public void SetClientName(string? clientName)
    {
        ClientName = clientName?.ToUpper();
    }

    public void SetValue(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("Valor deve ser maior que zero.");
        Value = value;
    }

    public void SetFreight(decimal freight)
    {
        Freight = freight;
    }

    public void SetFinantialDiscount(decimal finantialDiscount)
    {
        FinantialDiscount = finantialDiscount;
    }

    public void SetObservation(string? observation)
    {
        Observation = observation;
    }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    public void AddItem(EstimateItem item)
    {
        Items ??= [];
        Items.Add(item);
    }

    public void AddRangeItems(IEnumerable<EstimateItem> items)
    {
        Items ??= [];
        foreach (var item in items)
            Items.Add(item);
    }

    public void AddRangeReceipts(IEnumerable<EstimateReceipt> receipts)
    {
        Receipts ??= [];
        foreach (var receipt in receipts)
            Receipts.Add(receipt);
    }

    public void SetFreightId(Guid freightId)
    {
        FreightId = freightId;
    }

    public void ClearItems()
    {
        Items.Clear();
    }

    public void SetCreatedAt(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    public void ValidateTotals()
    {
        if (FinantialDiscount > Value)
            throw new BusinessRuleException("O desconto não pode ser maior que o valor do orçamento");
    }

    public void SetEstimateStatus(EstimateStatus estimateStatus)
    {
        EstimateStatus = estimateStatus;
    }
}