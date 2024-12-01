namespace Magnus.Core.Entities;

public class Payment : EntityBase
{
    public string Name { get; private set; }

    private Payment(){}
    public Payment(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException("informe o nome");
        Name = name;
    }
    
}