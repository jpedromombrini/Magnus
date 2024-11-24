namespace Magnus.Core.Entities;

public class Receipt : EntityBase
{
    public string Name { get; private set; }
    public decimal Increase { get; private set; }
    public bool InIstallments { get; private set; }
    private Receipt(){}
    public Receipt(string name, decimal increase, bool inIstallments)
    {
        SetName(name);
        SetIncrease(increase);
        SetInIstallments(inIstallments);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O nome não pode ser nulo ou vazio.");
        Name = name;
    }

    public void SetIncrease(decimal increase)
    {
        if (increase < 0)
            throw new ArgumentNullException("Acréscimo não pode ser negativo.");
        Increase = increase;
    }

    public void SetInIstallments(bool inIstallments)
    {
        InIstallments = inIstallments;
    }
}