using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class AccountsReceivableMapper
{
    #region Request

    public static AccountsReceivable MapToEntity(this CreateAccountsReceivableRequest request)
    {
        var account = new AccountsReceivable(request.ClientId, request.SaleReceiptInstallmentId, request.Document,
            request.DueDate, request.Value, request.Interest, request.Discount, request.Installment,
            request.TotalInstallment, request.CostCenterId);
        if (!string.IsNullOrEmpty(request.Observation))
            account.SetObservation(request.Observation);
        return account;
    }

    public static AccountsReceivable MapToEntity(this UpdateAccountsReceivableRequest request)
    {
        var account = new AccountsReceivable(request.ClientId, request.SaleReceiptInstallmentId, request.Document,
            request.DueDate, request.Value, request.Interest, request.Discount, request.Installment,
            request.TotalInstallment, request.CostCenterId);
        if (!string.IsNullOrEmpty(request.Observation))
            account.SetObservation(request.Observation);
        account.SetReceiptDate(request.ReceiptDate);
        account.SetReceiptValue(request.ReceiptValue);
        account.SetReceiptId(request.ReceiptId);
        return account;
    }

    public static IEnumerable<AccountsReceivable> MapToEntity(
        this IEnumerable<CreateAccountsReceivableRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    public static IEnumerable<AccountsReceivable> MapToEntity(
        this IEnumerable<UpdateAccountsReceivableRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    #endregion

    #region Response

    public static AccountsReceivableResponse MapToResponse(this AccountsReceivable entity)
    {
        var accountResponse = new AccountsReceivableResponse(entity.Id, entity.CreatedAt,
            entity.SaleReceiptInstallmentId, entity.Client.MapToResponse(), entity.Document, entity.DueDate,
            entity.DueDate, entity.ReceiptId, entity.ReceiptValue, entity.Value, entity.Interest, entity.Discount,
            entity.Installment, entity.TotalInstallment, entity.CostCenter.MapToResponse(), entity.Observation,
            entity.Status);
        return accountResponse;
    }

    public static IEnumerable<AccountsReceivableResponse> MapToResponse(this IEnumerable<AccountsReceivable> entity)
    {
        return entity.Select(MapToResponse).ToList();
    }

    #endregion
}