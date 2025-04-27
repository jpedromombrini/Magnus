using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record AccountsReceivableResponse(  DateTime CreatedAt,
    Guid Id,
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
    string? Observation,
    AccountsReceivableStatus Status);