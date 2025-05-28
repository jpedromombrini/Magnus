using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class SaleReceiptMapper
{
    #region Request

    public static SaleReceipt MapToEntity(this CreateSaleReceiptRequest request)
    {
        var saleReceipt = new SaleReceipt(request.ClientId, request.UserId, request.ReceiptId);
        foreach (var installment in request.Installments)
        {
            var installmentToadd = new SaleReceiptInstallment(installment.DueDate, installment.PaymentDate,
                installment.PaymentValue, installment.Value,
                installment.Discount,
                installment.Interest,
                installment.Installment, installment.ProofImage);
            saleReceipt.AddInstallment(installmentToadd);
        }

        return saleReceipt;
    }

    public static SaleReceipt MapToEntity(this UpdateSaleReceiptRequest request)
    {
        var saleReceipt = new SaleReceipt(request.ClientId, request.UserId, request.ReceiptId);
        foreach (var installment in request.Installments)
        {
            var installmentToAdd = new SaleReceiptInstallment(installment.DueDate, installment.PaymentDate, installment.PaymentValue, installment.Value,
                installment.Discount,
                installment.Interest,
                installment.Installment, installment.ProofImage);
            saleReceipt.AddInstallment(installmentToAdd);
        }

        return saleReceipt;
    }

    public static IEnumerable<SaleReceipt> MapToEntity(this IEnumerable<CreateSaleReceiptRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    public static IEnumerable<SaleReceipt> MapToEntity(this IEnumerable<UpdateSaleReceiptRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    #endregion

    #region Response

    public static IEnumerable<SaleReceiptResponse> MapToResponse(this IEnumerable<SaleReceipt> saleReceipts)
    {
        return saleReceipts.Select(MapToResponse).ToList();
    }

    public static SaleReceiptResponse MapToResponse(this SaleReceipt saleReceipt)
    {
        var installments = new List<SaleReceiptInstallmentResponse>(saleReceipt.Installments.Count);
        installments.AddRange(saleReceipt.Installments.Select(installment =>
            new SaleReceiptInstallmentResponse(installment.Id, saleReceipt.Id, installment.DueDate,
                installment.PaymentDate, installment.Value, installment.Discount, installment.Interest,
                installment.Installment,
                installment.GetProofImageBase64())));

        return new SaleReceiptResponse(
            saleReceipt.Id,
            saleReceipt.ClienteId,
            saleReceipt.UserId,
            saleReceipt.SaleId,
            saleReceipt.ReceiptId,
            installments);
    }

    #endregion
}