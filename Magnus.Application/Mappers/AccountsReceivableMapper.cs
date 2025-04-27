using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Microsoft.Extensions.Logging.Console;

namespace Magnus.Application.Mappers;

public static class AccountsReceivableMapper
{
    #region Request

    public static AccountsReceivable MapToEntity(this CreateAccountsReceivableRequest request)
    {
        var account = new AccountsReceivable(request.SaleReceiptInstallmentId, request.ClientId, request.ClientName,
            request.Document,
            request.DueDate, request.PaymentDate,
            request.PaymentValue, request.Value, request.Interest, request.Discount, request.Installment,
            request.CostCenter);
        if (!string.IsNullOrEmpty(request.Observation))
            account.SetObservation(request.Observation);
        return account;
    }

    public static AccountsReceivable MapToEntity(this UpdateAccountsReceivableRequest request)
    {
        var account = new AccountsReceivable(request.SaleReceiptInstallmentId, request.ClientId, request.ClientName,
            request.Document, request.DueDate, request.PaymentDate,
            request.PaymentValue, request.Value, request.Interest, request.Discount, request.Installment,
            request.CostCenter);
        if (!string.IsNullOrEmpty(request.Observation))
            account.SetObservation(request.Observation);
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
        var accountResponse = new AccountsReceivableResponse(entity.CreatedAt, entity.Id,
            entity.SaleReceiptInstallmentId,
            entity.ClientId, entity.ClientName, entity.Document,
            entity.DueDate, entity.PaymentDate,
            entity.PaymentValue, entity.Value, entity.Interest, entity.Discount, entity.Installment,
            entity.CostCenter, entity.Observation, entity.Status);
        return accountResponse;
    }

    public static IEnumerable<AccountsReceivableResponse> MapToResponse(this IEnumerable<AccountsReceivable> entity)
    {
        return entity.Select(MapToResponse).ToList();
    }

    #endregion
}