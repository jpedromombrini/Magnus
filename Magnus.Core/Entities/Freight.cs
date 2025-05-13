namespace Magnus.Core.Entities;

public class Freight : EntityBase
{
    public string Name { get; private set; }

    private Freight(){}
    public Freight(string name)
    {
        Name = name;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Informe o nome");
        Name = name;
    }
}