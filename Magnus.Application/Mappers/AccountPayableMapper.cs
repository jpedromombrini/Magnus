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
            request.Value, request.Discount, request.Interest,
            request.CostCenterId, request.Installment, request.InvoiceId, request.UserPaymentId, request.LaboratoryId,
            request.PaymentId, request.TotalInstallment, request.Reference);
    }

    public static List<AccountsPayable> MapToEntity(this List<CreateAccountsPayableRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    public static AccountsPayable MapToEntity(this UpdateAccountsPayableRequest request)
    {
        return new AccountsPayable(request.Document, request.SupplierId, DateTime.Now, request.DueDate,
            request.Value, request.Discount, request.Interest,
            request.CostCenterId, request.Installment, request.InvoiceId, request.UserPaymentId, request.LaboratoryId,
            request.PaymentId, request.TotalInstallment, request.Reference);
    }

    #endregion

    #region Responses

    public static AccountsPayableResponse MapToResponse(this AccountsPayable entity)
    {
        return new AccountsPayableResponse(entity.Id, entity.Document, entity.SupplierId, entity.SupplierName,
            entity.CreatedAt, entity.DueDate, entity.PaymentDate, entity.Value, entity.PaymentValue,
            entity.Discount, entity.Interest, entity.CostCenterId, entity.Installment, entity.InvoiceId,
            entity.UserPaymentId, entity.PaymentId, entity.LaboratoryId, (int)entity.AccountPayableStatus,
            entity.Payment.MapToResponse(), entity.TotalInstallment, entity.Reference, entity.GetProofImageBase64());
    }

    public static IEnumerable<AccountsPayableResponse> MapToResponse(this IEnumerable<AccountsPayable> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}