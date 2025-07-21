using Magnus.Core.ValueObjects;

namespace Magnus.Core.Entities;

public class ClientPhone : EntityBase
{
    private ClientPhone()
    {
    }

    public ClientPhone(Phone phone, string description)
    {
        SetPhone(phone);
        SetDescription(description);
    }

    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }
    public Phone Phone { get; private set; }
    public string? Description { get; private set; }

    public void SetClientId(Guid clientId)
    {
        if (clientId == Guid.Empty)
            throw new ArgumentException("O Id do cliente não pode ser vazio.");
        ClientId = clientId;
    }

    public void SetClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        Client = client;
    }

    public void SetPhone(Phone phone)
    {
        Phone = phone;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }
}