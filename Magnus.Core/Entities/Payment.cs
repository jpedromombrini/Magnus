namespace Magnus.Core.Entities;

public class Payment : EntityBase
{
    private Payment()
    {
    }

    public Payment(string name)
    {
        SetName(name);
    }

    public string Name { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("informe o nome");
        Name = name.ToUpper();
    }
}