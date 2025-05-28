using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class EstimateMapper
{
    #region Request

    public static Estimate MapToEntity(this CreateEstimateRequest request)
    {
        var estimate = new Estimate(request.Description, request.ValiditAt, request.ClientId, request.ClientName,
            request.Value,
            request.Freight, request.FinantialDiscount, request.UserId);
        if (string.IsNullOrEmpty(request.Observation))
            estimate.SetObservation("");
        if(request.FreightId is not null)
            estimate.SetFreightId((Guid)request.FreightId);
        estimate.SetObservation(request.Observation);
        estimate.AddRangeItems(request.Items.MapToEntity());
        if(request.Receipts is not null)
            estimate.AddRangeReceipts(request.Receipts.MapToEntity());
        return estimate;
    }

    public static Estimate MapToEntity(this UpdateEstimateRequest request)
    {
        var estimate = new Estimate(request.Description, request.ValiditAt, request.ClientId, request.ClientName,
            request.Value,
            request.Freight, request.FinantialDiscount, request.UserId);
        if (string.IsNullOrEmpty(request.Observation))
            estimate.SetObservation("");
        if(request.FreightId is not null)
            estimate.SetFreightId((Guid)request.FreightId);
        estimate.SetObservation(request.Observation);
        estimate.AddRangeItems(request.Items.MapToEntity());
        if(request.Receipts is not null)
            estimate.AddRangeReceipts(request.Receipts.MapToEntity());
        return estimate;
    }

    public static EstimateReceipt MapToEntity(this EstimateReceiptRequest request)
    {
        var estimateReceipt = new EstimateReceipt(request.UserId, request.ReceiptId);
        estimateReceipt.SetClientId(request.ClienteId);
        estimateReceipt.AddInstallments(request.Installments.MapToEntity());
        return estimateReceipt;
    }

    public static IEnumerable<EstimateReceipt> MapToEntity(this IEnumerable<EstimateReceiptRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static EstimateReceiptInstallment MapToEntity(this EstimateReceiptInstallmentRequest request)
    {
        return new EstimateReceiptInstallment(request.DueDate, request.PaymentDate,
            request.PaymentValue, request.Value, request.Discount, request.Interest, request.Installment);
    }

    public static IEnumerable<EstimateReceiptInstallment> MapToEntity(
        this IEnumerable<EstimateReceiptInstallmentRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static EstimateItem MapToEntity(this EstimateItemRequest request)
    {
        return new EstimateItem(request.ProductId, request.ProductName, request.Amount, request.Value,
            request.TotalValue, request.Discount);
    }

    public static IEnumerable<EstimateItem> MapToEntity(this IEnumerable<EstimateItemRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region response

    public static EstimateResponse MapToResponse(this Estimate entity)
    {
        var user = entity.User.MapToResponse();
        var estimate = new EstimateResponse(entity.Id, entity.Code, entity.Description, entity.CreatedAt,
            entity.ValiditAt, entity.ClientId, entity.ClientName, entity.Value, entity.FreightId, entity.Freight,
            entity.FinantialDiscount, entity.Observation, entity.UserId, user, entity.Items.MapToResponse(),
            entity.Receipts.MapToResponse(), entity.EstimateStatus);
        return estimate;
    }

    public static EstimateItemResponse MapToResponse(this EstimateItem entity)
    {
        return new EstimateItemResponse(entity.Id, entity.ProductId, entity.ProductName, entity.Amount,
            entity.TotalValue, entity.Value, entity.Discount);
    }

    public static IEnumerable<EstimateResponse> MapToResponse(this IEnumerable<Estimate> entities)
    {
        return entities.Select(MapToResponse);
    }

    public static IEnumerable<EstimateItemResponse> MapToResponse(this IEnumerable<EstimateItem> entities)
    {
        return entities.Select(MapToResponse);
    }

    public static EstimateReceiptResponse MapToResponse(this EstimateReceipt entity)
    {
        return new EstimateReceiptResponse(entity.Id, entity.ClienteId, entity.UserId, entity.EstimateId,
            entity.ReceiptId, entity.Installments.MapToResponse());
    }

    public static EstimateReceiptInstallmentResponse MapToResponse(this EstimateReceiptInstallment entity)
    {
        return new EstimateReceiptInstallmentResponse(entity.Id, entity.EstimateReceiptId, entity.DueDate,
            entity.PaymentDate, entity.PaymentValue, entity.Value, entity.Discount, entity.Interest,
            entity.Installment);
    }

    public static IEnumerable<EstimateReceiptInstallmentResponse> MapToResponse(
        this IEnumerable<EstimateReceiptInstallment> entities)
    {
        return entities.Select(MapToResponse);
    }

    public static IEnumerable<EstimateReceiptResponse> MapToResponse(
        this IEnumerable<EstimateReceipt> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}