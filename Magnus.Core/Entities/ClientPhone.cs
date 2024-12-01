using System.Text.RegularExpressions;
using Magnus.Core.Enumerators;
using Magnus.Core.ValueObjects;

namespace Magnus.Core.Entities;

public class ClientPhone : EntityBase
{
    public Guid ClientId { get; private set; }
    public Phone Phone { get; private set; }
    public string? Description { get; private set; }

    private ClientPhone(){}
    public ClientPhone(Guid clientId, Phone phone, string description)
    {
        SetClientId(clientId);
        SetPhone(phone);
        SetDescription(description);
    }
    public void SetClientId(Guid clientId)
    {
        if(clientId == Guid.Empty)
            throw new ArgumentException("O Id do cliente não pode ser vazio.");
        ClientId = clientId;
    }

    public void SetPhone(Phone phone)
    {
        Phone = phone;
    }
    public void SetDescription(string description)
    {
        Description = description;
    }
}