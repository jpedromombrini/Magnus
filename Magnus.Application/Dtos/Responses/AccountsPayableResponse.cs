namespace Magnus.Application.Dtos.Responses;

public record AccountsPayableResponse(
int Document, 
Guid SupplierId,
string SupplierName,
 DateTime CreatedAt,
 DateTime DueDate,
 DateTime PaymentDate,
 decimal Value,
 decimal PaymentValue,
 decimal Discount,
 decimal Interest,
 string CostCenter,
 int Installment,
 Guid? InvoiceId,
 Guid? UserPaymentId,
 bool Canceled,
 IEnumerable<AccountsPayableOccurenceResponse>? Occurrences);