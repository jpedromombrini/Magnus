using Magnus.Application.Dtos.Requests;
using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Responses;

public record InvoiceResponse(
    Guid Id,
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
    IEnumerable<InvoiceItemResponse> Items,
    IEnumerable<InvoicePaymentResponse>? Payments,
    Guid? DoctorId,
    bool UpdateFinantial,
    Guid? CostCenterId,
    Guid LaboratoryId);