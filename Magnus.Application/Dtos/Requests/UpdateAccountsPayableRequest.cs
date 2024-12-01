namespace Magnus.Application.Dtos.Requests;

public record UpdateAccountsPayableRequest(
    Guid UserId,
    int Document,
    Guid SupplierId,
    DateTime DueDate,
    DateTime PaymentDate,
    decimal Value,
    decimal PaymentValue,
    decimal Discount,
    decimal Interest,
    string CostCenter);