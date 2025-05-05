using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record CreateInvoiceRequest(
    int Number,
    int Serie,
    string Key,
    DateTime DateEntry,
    DateTime Date,
    Guid SupplierId,
    string SupplierName,
    decimal Freight,
    decimal Deduction,
    decimal Value,
    string Observation,
    InvoiceSituation InvoiceSituation,
    IEnumerable<InvoiceItemRequest> Items,
    InvoicePaymentRequest Payment,
    Guid? DoctorId);