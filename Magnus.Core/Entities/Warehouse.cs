namespace Magnus.Core.Entities;

public class Warehouse : EntityBase
{
    private Warehouse()
    {
    }

    public Warehouse(string name, User user)
    {
        SetName(name);
        SetUser(user);
    }

    public int Code { get; private set; }
    public string Name { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O nome não pode ser nulo ou vazio.");
        Name = name.ToUpper();
    }

    public void SetUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        if (user.Id == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do usuário");
        UserId = user.Id;
        User = user;
    }

    public void SetCode(int code)
    {
        Code = code;
    }
}