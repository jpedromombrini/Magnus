using Magnus.Core.Enumerators;
using Magnus.Core.ValueObjects;

namespace Magnus.Core.Entities;

public class User : EntityBase
{
    public Email Email { get; private set; }
    public string Password { get; private set; } 
    public string Name { get; private set; } 
    public DateTime InitialDate { get; private set; }
    public DateTime FinalDate { get; private set; }
    public bool Active { get; private set; }
    public UserType UserType { get;  private set; }
    public Warehouse? Warehouse { get; private set; }
    private User() {}
    
    public User(Email email, string password, string name, DateTime initialDate, DateTime finalDate, bool active, UserType userType)
    {
        SetEmail(email);
        SetPassword(password);
        SetName(name);
        SetInitialDate(initialDate);
        SetFinalDate(finalDate);
        SetActive(active);
        SetUserType(userType);
    }

    public void SetEmail(Email email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Senha não pode ser nula ou vazia.", nameof(password));
        Password = password;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.", nameof(name));
        Name = name;
    }

    public void SetInitialDate(DateTime initialDate)
    {
        if (initialDate > DateTime.Now)
            throw new ArgumentException("A data inicial não pode ser no futuro.", nameof(initialDate));
        InitialDate = initialDate;
    }

    public void SetFinalDate(DateTime finalDate)
    {
        if (finalDate < InitialDate)
            throw new ArgumentException("A data final não pode ser anterior à data inicial.", nameof(finalDate));
        FinalDate = finalDate;
    }

    public void SetActive(bool active)
    {
        Active = active;
    }

    public void SetUserType(UserType userType)
    {
        UserType = userType;
    }

    public void SetWarehouse(Warehouse? warehouse)
    {
        Warehouse = warehouse;
    }
    
}