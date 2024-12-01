namespace Magnus.Application.Dtos.Requests;

public record CreateAccountsPayableRequest(
    Guid UserId,
    Guid SupplierId,
    string CostCenterCode,
    int Document,
    List<AccountsPayableRequest> AccountsPayableRequests);