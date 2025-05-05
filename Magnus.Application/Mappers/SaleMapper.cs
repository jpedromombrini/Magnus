using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class SaleMapper
{
    #region Request

    public static Sale MapToEntity(this CreateSaleRequest request)
    {
        var sale = new Sale(request.ClientId, request.UserId, request.Value, request.Freight,
            request.FinantialDiscount);
        if (request.Receipts is not null)
            sale.AddRangeReceipts(request.Receipts.MapToEntity());
        sale.AddItems(request.Items.MapToEntity());
        return sale;
    }

    public static Sale MapToEntity(this UpdateSaleRequest request)
    {
        var sale = new Sale(request.ClientId, request.UserId, request.Value, request.Freight,
            request.FinantialDiscount);
        if (request.Receipts is not null)
            sale.AddRangeReceipts(request.Receipts.MapToEntity());
        sale.AddItems(request.Items.MapToEntity());
        return sale;
    }

    public static SaleItem MapToEntity(this SaleItemRequest request)
    {
        return new SaleItem(request.ProductId, request.ProductName, request.Amount, request.Value, request.TotalPrice,
            request.Discount);
    }

    public static IEnumerable<SaleItem> MapToEntity(this IEnumerable<SaleItemRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static SaleResponse MapToResponse(this Sale entity)
    {
        return new SaleResponse(entity.Id, entity.CreateAt, entity.Document, entity.ClientId, entity.ClientName,
            entity.UserId,
            entity.Value, entity.Freight, entity.FinantialDiscount, entity.Status, entity.Items.MapToResponse(),
            entity.Receipts is null ? null : entity.Receipts.MapToResponse());
    }

    public static SaleItemResponse MapToResponse(this SaleItem entity)
    {
        return new SaleItemResponse(entity.Id, entity.ProductId, entity.ProductName, entity.Amount, entity.Value,
            entity.TotalPrice, entity.Discount);
    }

    public static IEnumerable<SaleItemResponse> MapToResponse(this IEnumerable<SaleItem> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}