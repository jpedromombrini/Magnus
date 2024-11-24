using Magnus.Core.ValueObjects;

namespace Magnus.Core.Entities;

public class Seller : EntityBase
{
    public string Name { get; private set; }
    public Document Document { get; private set; }
    public Phone Phone { get; private set; }
    public Email Email { get; private set; }
    public Guid UserId { get; private set; }

    private Seller(){}
    public Seller(string name, Document document, Phone phone, Email email, Guid userId)
    {
        SetName(name);
        SetDocument(document);
        SetPhone(phone);
        SetEmail(email);
        SetUserId(userId);
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

    public void SetPhone(Phone phone)
    {
        Phone = phone;
    }

    public void SetEmail(Email email)
    {
        Email = email;
    }

    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException("Informe o Id do usuário");
        UserId = userId;
    }
}