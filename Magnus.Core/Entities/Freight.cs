namespace Magnus.Core.Entities;

public class Freight : EntityBase
{
    private Freight()
    {
    }

    public Freight(string name)
    {
        SetName(name);
    }

    public string Name { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Informe o nome");
        Name = name.ToUpper();
    }
}