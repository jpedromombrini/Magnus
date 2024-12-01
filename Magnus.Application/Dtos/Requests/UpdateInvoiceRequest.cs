using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests;

public record UpdateInvoiceRequest(  int Number,
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
    List<InvoiceItemRequest> Items,
    Guid? DoctorId);