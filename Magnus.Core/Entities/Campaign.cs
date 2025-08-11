namespace Magnus.Core.Entities;

public class Campaign : EntityBase
{
    public Campaign(string name, string? description, DateOnly initialDate, DateOnly finalDate, bool active)
    {
        SetName(name);
        SetDescription(description);
        SetInitialDate(initialDate);
        SetFinalDate(finalDate);
        SetActive(active);
        Items = [];
    }

    private Campaign()
    {
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateOnly InitialDate { get; private set; }
    public DateOnly FinalDate { get; private set; }
    public bool Active { get; private set; }
    public ICollection<CampaignItem> Items { get; }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Nome não pode ser nulo");
        Name = name;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetInitialDate(DateOnly initialDate)
    {
        InitialDate = initialDate;
    }

    public void SetFinalDate(DateOnly finalDate)
    {
        FinalDate = finalDate;
    }

    public void SetActive(bool active)
    {
        Active = active;
    }

    public void AddItem(CampaignItem item)
    {
        Items.Add(item);
    }

    public void RemoveItem(CampaignItem item)
    {
        Items.Remove(item);
    }
}