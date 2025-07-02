namespace Magnus.Core.Entities;

public class Doctor : EntityBase
{
    private Doctor()
    {
    }

    public Doctor(int code, string name, string crm)
    {
        SetCode(code);
        SetName(name);
        SetCrm(crm);
    }

    public int Code { get; private set; }
    public string Name { get; private set; }
    public string Crm { get; private set; }

    public void SetCode(int code)
    {
        if (code <= 0)
            throw new ArgumentException("O Código deve ser maior que zero.");
        Code = code;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome não pode ser nulo ou com espaços em branco.");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("O nome não pode ser nulo ou vazio.");
        Name = name.ToUpper();
    }

    public void SetCrm(string crm)
    {
        if (string.IsNullOrWhiteSpace(crm))
            throw new ArgumentException("O crm não pode ser nulo ou com espaços em branco.");
        if (string.IsNullOrEmpty(crm))
            throw new ArgumentException("O crm não pode ser nulo ou vazio.");
        Crm = crm;
    }
}