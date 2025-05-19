using System.Diagnostics;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class InvoiceMapper
{
    #region Request

    public static Invoice MapToEntity(this CreateInvoiceRequest request)
    {
        var invoice = new Invoice(request.Number, request.Serie, request.Key, request.DateEntry, request.Date,
            request.SupplierId, request.SupplierName, request.Freight, request.Deduction, request.Value,
            request.Observation, request.InvoiceSituation, request.DoctorId, request.UpdateFinantial,
            request.CostCenterId, request.LaboratoryId);
        invoice.SetItems(request.Items.MapToEntity());
        if (!request.UpdateFinantial) return invoice;
        if (request.Payments != null)
            invoice.AddRangePayments(request.Payments.MapToEntity());
        return invoice;
    }

    public static Invoice MapToEntity(this UpdateInvoiceRequest request)
    {
        var invoice = new Invoice(request.Number, request.Serie, request.Key, request.DateEntry, request.Date,
            request.SupplierId, request.SupplierName, request.Freight, request.Deduction, request.Value,
            request.Observation, request.InvoiceSituation, request.DoctorId, request.UpdateFinantial,
            request.CostCenterId, request.LaboratoryId);
        invoice.SetItems(request.Items.MapToEntity());
        if (!request.UpdateFinantial) return invoice;
        if (request.Payments != null)
            invoice.AddRangePayments(request.Payments.MapToEntity());
        return invoice;
    }

    public static InvoicePayment MapToEntity(this InvoicePaymentRequest request)
    {
        var invoicePayment = new InvoicePayment(request.PaymentId, request.SupplierId);
        foreach (var installment in request.Installments)
        {
            var installmentToAdd = new InvoicePaymentInstallment(installment.DueDate, installment.PaymentDate,
                installment.Value,
                installment.Discount,
                installment.Interest,
                installment.Installment);
            installmentToAdd.SetInvoicePayment(invoicePayment);
            invoicePayment.AddInstallment(installmentToAdd);
        }

        return invoicePayment;
    }

    public static IEnumerable<InvoicePayment> MapToEntity(this IEnumerable<InvoicePaymentRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static InvoiceItem MapToEntity(this InvoiceItemRequest request)
    {
        return new InvoiceItem(request.ProductId, request.ProductInternalCode, request.ProductName, request.Amount,
            request.TotalValue, request.Bonus, request.Validate, request.Lot);
    }

    public static IEnumerable<InvoiceItem> MapToEntity(this IEnumerable<InvoiceItemRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static InvoiceResponse MapToResponse(this Invoice entity)
    {
        return new InvoiceResponse(
            entity.Id, entity.Number, entity.Serie, entity.Key, entity.DateEntry,
            entity.Date, entity.SupplierId, entity.SupplierName, entity.Freight, entity.Deduction, entity.Value,
            entity.Observation, entity.InvoiceSituation, entity.Items.MapToResponse(),
            entity.InvoicePayments.MapToResponse(), entity.DoctorId, entity.UpdateFinantial, entity.CostCenterId,
            entity.LaboratoryId);
    }

    public static IEnumerable<InvoiceResponse> MapToResponse(this IEnumerable<Invoice> entities)
    {
        return entities.Select(MapToResponse);
    }

    public static InvoiceItemResponse MapToResponse(this InvoiceItem entity)
    {
        return new InvoiceItemResponse(entity.Id, entity.ProductId, entity.ProductInternalCode, entity.ProductName,
            entity.Amount, entity.TotalValue, entity.Validate, entity.Lot, entity.Bonus);
    }

    public static IEnumerable<InvoiceItemResponse> MapToResponse(this IEnumerable<InvoiceItem> entities)
    {
        return entities.Select(MapToResponse);
    }

    public static InvoicePaymentResponse MapToResponse(this InvoicePayment entity)
    {
        return new InvoicePaymentResponse(entity.Id, entity.InvoiceId, entity.PaymentId, entity.Payment.MapToResponse(),
            entity.SupplierId, entity.Installments.MapToResponse());
    }

    public static IEnumerable<InvoicePaymentResponse> MapToResponse(this IEnumerable<InvoicePayment>? entities)
    {
        return entities == null ? [] : entities.Select(MapToResponse);
    }

    public static InvoicePaymentInstallmentResponse MapToResponse(this InvoicePaymentInstallment entity)
    {
        return new InvoicePaymentInstallmentResponse(entity.Id, entity.InvoicePaymentId, entity.DueDate,
            entity.PaymentDate, entity.Value, entity.Discount, entity.Interest, entity.Installment);
    }

    public static IEnumerable<InvoicePaymentInstallmentResponse> MapToResponse(
        this IEnumerable<InvoicePaymentInstallment> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}