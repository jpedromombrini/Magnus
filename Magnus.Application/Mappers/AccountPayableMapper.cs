using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class AccountPayableMapper
{
    #region Requests

    public static AccountsPayable MapToEntity(this CreateAccountsPayableRequest request)
    {
        return new AccountsPayable(request.Document, request.SupplierId, DateTime.Now, request.DueDate,
            request.PaymentDate, request.Value, request.PaymentValue, request.Discount, request.Interest,
            request.CostCenterId, request.Installment, request.InvoiceId, request.UserPaymentId, request.LaboratoryId,
            request.PaymentId);
    }

    public static AccountsPayable MapToEntity(this UpdateAccountsPayableRequest request)
    {
        return new AccountsPayable(request.Document, request.SupplierId, DateTime.Now, request.DueDate,
            request.PaymentDate, request.Value, request.PaymentValue, request.Discount, request.Interest,
            request.CostCenterId, request.Installment, request.InvoiceId, request.UserPaymentId, request.LaboratoryId,
            request.PaymentId);
    }

    #endregion

    #region Responses

    public static AccountsPayableResponse MapToResponse(this AccountsPayable entity)
    {
        return new AccountsPayableResponse(entity.Id, entity.Document, entity.SupplierId, entity.SupplierName,
            entity.CreatedAt, entity.DueDate, entity.PaymentDate, entity.Value, entity.PaymentValue,
            entity.Discount, entity.Interest, entity.CostCenterId, entity.Installment, entity.InvoiceId,
            entity.UserPaymentId, entity.PaymentId, entity.LaboratoryId, (int)entity.AccountPayableStatus,
            entity.Payment.MapToResponse());
    }

    public static IEnumerable<AccountsPayableResponse> MapToResponse(this IEnumerable<AccountsPayable> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}