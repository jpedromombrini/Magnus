namespace Magnus.Application.Dtos.Requests;

public record UpdateAccountsReceivableRequest(
    Guid? SaleReceiptInstallmentId,
    Guid ClientId,
    int Document,
    DateOnly DueDate,
    Guid? ReceiptId,
    DateTime? ReceiptDate,
    decimal ReceiptValue,
    decimal Value,
    decimal Interest,
    decimal Discount,
    int Installment,
    int TotalInstallment,
    Guid? CostCenterId,
    string? Observation);