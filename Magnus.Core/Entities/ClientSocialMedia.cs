namespace Magnus.Core.Entities;

public class ClientSocialMedia : EntityBase
{
    public Guid ClientId { get; private set; }
    public string Name { get; private set; } 
    public string Link { get; private set; }

    private ClientSocialMedia() {}
    public ClientSocialMedia(Guid clientId, string name, string link)
    {
        ClientId = clientId;
        Name = name;
        Link = link;
    }

    public void SetClientId(Guid clientId)
    {
        if(clientId == Guid.Empty)
            throw new ArgumentException("O Id do cliente não pode ser vazio.");
        ClientId = clientId;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O número não pode ser vazio.");
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O número não pode ser vazio.");
        Name = name;
    }
    
    public void SetLink(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("O Link não pode ser vazio.");
        if(string.IsNullOrWhiteSpace(link))
            throw new ArgumentException("O Link não pode ser vazio.");
        Link = link;
    }
}