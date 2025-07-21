using Magnus.Core.ValueObjects;

namespace Magnus.Core.Entities;

public class Client : EntityBase
{
    private Client()
    {
    }

    public Client(string name, Document document)
    {
        SetName(name);
        SetDocument(document);
    }

    public string Name { get; private set; }
    public Email? Email { get; private set; }
    public Document Document { get; private set; }
    public string? Occupation { get; private set; }
    public DateOnly? DateOfBirth { get; private set; }
    public Address? Address { get; private set; }
    public string? RegisterNumber { get; private set; }
    public List<ClientSocialMedia>? SocialMedias { get; private set; }
    public List<ClientPhone>? Phones { get; private set; }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        Name = name.ToUpper();
    }

    public void SetEmail(Email? email)
    {
        Email = email;
    }

    public void SetDocument(Document document)
    {
        Document = document;
    }

    public void SetOccupation(string? occupation)
    {
        Occupation = occupation;
    }

    public void SetDateOfBirth(DateOnly? dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public void SetAddress(Address? address)
    {
        Address = address;
    }

    public void SetRegisterNumber(string? registerNumber)
    {
        RegisterNumber = registerNumber ?? string.Empty;
    }

    public void AddPhone(ClientPhone phone)
    {
        Phones ??= [];
        Phones?.Add(phone);
    }

    public void AddSocialMedia(ClientSocialMedia socialMedia)
    {
        SocialMedias ??= [];
        SocialMedias.Add(socialMedia);
    }
}