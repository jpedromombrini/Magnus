namespace Magnus.Core.Entities;

public class Laboratory : EntityBase
{
    public int Code { get; private set; }
    public string Name { get; private set; } = "";

    private Laboratory() { }
    public Laboratory(int code, string name)
    {
        SetCode(code);
        SetName(name);
    }
    
    public void SetCode(int code)
    {
        if (code <= 0)
        {
            throw new ArgumentException("O código deve ser maior que zero.");
        }
        Code = code;
    }
    
    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name), "O nome não pode ser nulo ou vazio.");
        }
        Name = name;
    }
}