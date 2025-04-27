namespace Magnus.Application.Dtos.Requests;

public record UpdateAccountsReceivableRequest(
    DateTime CreatedAt,
    Guid? SaleReceiptInstallmentId,
    Guid ClientId,
    string ClientName,
    int Document,
    DateOnly DueDate,
    DateOnly? PaymentDate,
    decimal PaymentValue,
    decimal Value,
    decimal Interest,
    decimal Discount,
    int Installment,
    string CostCenter,
    string? Observation);