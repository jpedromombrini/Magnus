using Magnus.Core.Exceptions;

namespace Magnus.Core.Entities;

public class AccountsReceivableOccurence : EntityBase
{
    private AccountsReceivableOccurence()
    {
    }

    public AccountsReceivableOccurence(Guid accountsReceivableId, Guid userId, string occurrence)
    {
        SetCreatAt(DateTime.UtcNow);
        SetAccountsReceivableId(accountsReceivableId);
        SetUserId(userId);
        SetOccurrence(occurrence);
    }

    public DateTime CreatAt { get; private set; }
    public Guid AccountsReceivableId { get; private set; }
    public AccountsReceivable AccountsReceivable { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public string Occurrence { get; private set; }

    public void SetCreatAt(DateTime creatAt)
    {
        CreatAt = creatAt;
    }

    public void SetAccountsReceivableId(Guid accountsReceivableId)
    {
        AccountsReceivableId = accountsReceivableId;
    }

    public void SetAccountsReceivable(AccountsReceivable accountsReceivable)
    {
        AccountsReceivable = accountsReceivable;
    }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    public void SetUser(User user)
    {
        User = user;
    }

    public void SetOccurrence(string occurrence)
    {
        if (string.IsNullOrEmpty(occurrence))
            throw new BusinessRuleException("informe uma ocorrência");
        Occurrence = occurrence;
    }
}