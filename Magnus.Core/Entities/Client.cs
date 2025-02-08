using Magnus.Core.ValueObjects;

namespace Magnus.Core.Entities;

public class Client : EntityBase
{
    public string Name { get; private set; }
    public Email? Email { get; private set; } 
    public Document Document { get; private set; }    
    public string? Occupation { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public Address? Address { get; private set; }
    public string? RegisterNumber { get; private set; }
    public List<ClientSocialMedia>? SocialMedias { get; private set; }
    public List<ClientPhone>? Phones { get; private set; } 

    private Client(){}
    public Client(string name, Document document)
    {
        SetName(name);
        SetDocument(document);
    }
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        Name = name;
    }
    
    public void SetEmail(Email email)
    {
        Email = email;
    }
    
    public void SetDocument(Document document)
    {
        Document = document;
    }
    
    public void SetOccupation(string occupation)
    {
        if (string.IsNullOrWhiteSpace(occupation))
            throw new ArgumentException("Ocupação não pode ser nula ou vazia.");
        Occupation = occupation;
    }

    public void SetDateOfBirth(DateOnly dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public void SetAddress(Address? address)
    {
        Address = address;
    }
    public void SetRegisterNumber(string registerNumber)
    {
        RegisterNumber = registerNumber ?? string.Empty; 
    }
    
    public void SetSocialMedias(List<ClientSocialMedia>? socialMedias)
    {
        SocialMedias = socialMedias ?? throw new ArgumentNullException(nameof(socialMedias), "Redes sociais não podem ser nulas.");
    }
    
    public void SetPhones(List<ClientPhone>? phones)
    {
        Phones = phones ?? throw new ArgumentNullException(nameof(phones), "Telefones não podem ser nulos.");
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