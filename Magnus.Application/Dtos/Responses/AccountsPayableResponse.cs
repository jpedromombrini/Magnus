namespace Magnus.Application.Dtos.Responses;

public record AccountsPayableResponse(
    Guid Id,
    int Document,
    Guid SupplierId,
    string SupplierName,
    DateTime CreatedAt,
    DateOnly DueDate,
    DateTime? PaymentDate,
    decimal Value,
    decimal PaymentValue,
    decimal Discount,
    decimal Interest,
    Guid CostCenterId,
    int Installment,
    Guid? InvoiceId,
    Guid? UserPaymentId,
    Guid PaymentId,
    Guid? LaboratoryId,
    int AccountPayableStatus,
    PaymentResponse Payment,
    int TotalInstallment,
    DateOnly Reference);