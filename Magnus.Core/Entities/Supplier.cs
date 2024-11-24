using Magnus.Core.ValueObjects;

namespace Magnus.Core.Entities;

public class Supplier : EntityBase
{
    public string Name { get; set; } 
    public Document Document { get; set; }  
    public Email Email { get; set; }  
    public Phone Phone { get; set; }
    public Address Address { get; set; }
    private Supplier() { }
    public Supplier(string name, Document document, Email email, Phone phone, Address address)
    {
        SetName(name);
        SetDocument(document);
        SetEmail(email);
        SetPhone(phone);
        SetAddress(address);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O nome não pode ser nulo ou vazio.");
        Name = name;
    }

    public void SetDocument(Document document)
    {
        Document = document;
    }

    public void SetEmail(Email email)
    {
        Email = email;
    }

    public void SetPhone(Phone phone)
    {
        Phone = phone;
    }

    public void SetAddress(Address address)
    {
        Address = address;
    }
}