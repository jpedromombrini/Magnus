namespace Magnus.Core.Entities;

public class Estimate : EntityBase
{
    public int Code { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ValiditAt { get; private set; }
    public Guid? ClientId { get; private set; }
    public string? ClientName { get; private set; }
    public decimal Value { get; private set; }
    public decimal Freight { get; private set; }
    public string Observation { get; set; }
    public Guid UserId { get; private set; }
    public User User { get; set; }
    public List<EstimateItem> Items { get; private set; }

    private Estimate(){}
    public Estimate(string description, DateTime validitAt, Guid? clientId, string? clientName, decimal value, decimal freight, Guid userId)
    {
        SetCreatedAt(DateTime.Now);
        SetDescription(description);
        SetValidity(validitAt);
        SetClientId(clientId);
        SetClientName(clientName);
        SetValue(value);
        SetFreight(freight);
        SetUserId(userId);
        Items = [];
    }

    public void SetDescription(string description)
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
        ClientName = clientName;   
    }

    public void SetValue(decimal value)
    {
        if(value <= 0)
            throw new ArgumentException("Valor deve ser maior que zero.");
        Value = value;
    }

    public void SetFreight(decimal freight)
    {
        Freight = freight;
    }
    public void SetObservation(string observation)
    {
        Observation = observation;
    }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    public void AddItem(EstimateItem item)
    {
        Items.Add(item);
    }

    public void ClearItems()
    {
        Items.Clear();
    }

    public void SetCreatedAt(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }
    
}