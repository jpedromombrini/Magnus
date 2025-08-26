namespace Magnus.Application.Dtos.Requests;

public record AccountsReceivableRenegociateRequest(
    ReceiptAccountReceivableRequest ReceiptAccountReceivableRequest,
    IEnumerable<CreateAccountsReceivableRequest> AccountsReceivables);