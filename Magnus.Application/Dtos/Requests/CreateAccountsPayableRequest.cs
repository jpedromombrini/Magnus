namespace Magnus.Application.Dtos.Requests;

public record CreateAccountsPayableRequest(
    int Document,
    Guid SupplierId,
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
    Guid? LaboratoryId,
    Guid PaymentId,
    int TotalInstallment,
    DateOnly Reference);