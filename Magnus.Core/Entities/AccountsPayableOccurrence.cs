namespace Magnus.Core.Entities;

public class AccountsPayableOccurrence : EntityBase
{
    public Guid AccountsPayableId { get; private set; }
    public AccountsPayable AccountsPayable { get; private set; } 
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string Occurrence { get; private set; }

    private AccountsPayableOccurrence()
    {
    }

    public AccountsPayableOccurrence(
        AccountsPayable accountsPayable,
        Guid userId,
        string userName,
        string occurrence)
    {
        
        SetAccountsPayable(accountsPayable);
        SetUserId(userId);
        SetUserName(userName);
        SetOccurrence(occurrence);
    }
    public void SetAccountsPayable(AccountsPayable accountsPayable)
    {
        if(accountsPayable is null)
            throw new ArgumentNullException(nameof(accountsPayable), "Contas à pagar não pode ser nulo");
        if(accountsPayable.Id != Guid.Empty)
            throw new ArgumentNullException(nameof(accountsPayable.Id), "o Id de contas a pagar não pode ser vazio");
        AccountsPayable = accountsPayable;
        AccountsPayableId = accountsPayable.Id;
    }

    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId), "Id de usuário não pode ser nulo.");
        UserId = userId;
    }

    public void SetUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
            throw new ArgumentNullException(nameof(userName), "Informe o nome do usuario.");
        UserName = userName;
    }

    public void SetOccurrence(string occurrence)
    {
        if (string.IsNullOrEmpty(occurrence))
            throw new ArgumentNullException(nameof(occurrence), "Informe a Ocorrência");
        Occurrence = occurrence;
    }
}