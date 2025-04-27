namespace Magnus.Core.Entities;

public class AccountsReceivableOccurrence : EntityBase
{
    public Guid AccountsReceivableId { get; private set; }
    public string OccurenceType { get; private set; }
    public string OccurrenceDescription { get; private set; }

    private AccountsReceivableOccurrence(){}

    public AccountsReceivableOccurrence()
    {
        
    }
}