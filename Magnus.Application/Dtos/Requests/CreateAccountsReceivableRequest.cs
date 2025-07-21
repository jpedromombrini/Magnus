namespace Magnus.Application.Dtos.Requests;

public record CreateAccountsReceivableRequest(
    Guid? SaleReceiptInstallmentId,
    Guid ClientId,
    int Document,
    DateOnly DueDate,
    decimal Value,
    decimal Interest,
    decimal Discount,
    int Installment,
    int TotalInstallment,
    Guid CostCenterId,
    string? Observation);