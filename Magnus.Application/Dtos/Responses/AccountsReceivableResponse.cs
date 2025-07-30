using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record AccountsReceivableResponse(
    Guid Id,
    DateTime CreatedAt,
    Guid? SaleReceiptInstallmentId,
    ClientResponse Client,
    int Document,
    DateOnly DueDate,
    DateTime? ReceiptDate,
    Guid? ReceiptId,
    decimal ReceiptValue,
    decimal Value,
    decimal Interest,
    decimal Discount,
    int Installment,
    int TotalInstallment,
    CostCenterResponse? CostCenter,
    string? Observation,
    AccountsReceivableStatus Status,
    string? ProofImage);