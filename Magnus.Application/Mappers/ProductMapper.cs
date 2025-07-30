using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class ProductMapper
{
    #region Request

    public static Product MapToEntity(this CreateProductRequest request)
    {
        Product product = new(request.Name, request.Price, request.LaboratoryId, request.ApplyPriceRule);
        if (request.Bars is not null)
            product.AddBars(request.Bars.MapToEntity());
        if (request.ProductPriceTable is not null)
            product.AddProductPriceTables(request.ProductPriceTable.MapToEntity());
        return product;
    }

    public static Product MapToEntity(this UpdateProductRequest request)
    {
        Product product = new(request.Name, request.Price, request.LaboratoryId, request.ApplyPriceRule);
        if (request.ProductPriceTable is not null)
            product.AddProductPriceTables(request.ProductPriceTable.MapToEntity());
        if (request.Bars is not null)
            product.AddBars(request.Bars.MapToEntity());
        return product;
    }

    public static Bar MapToEntity(this BarRequest request)
    {
        return new Bar(request.Code);
    }

    public static ProductPriceTable MapToEntity(this ProductPriceTableRequest request)
    {
        return new ProductPriceTable(request.MinimalAmount, request.MaximumAmount, request.Price);
    }

    public static IEnumerable<ProductPriceTable> MapToEntity(this IEnumerable<ProductPriceTableRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    public static IEnumerable<Bar> MapToEntity(this IEnumerable<BarRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion

    #region Response

    public static ProductResponse MapToResponse(this Product entity)
    {
        IEnumerable<BarResponse>? bars = null;
        IEnumerable<ProductPriceTableResponse>? prices = null;
        if (entity.Bars is not null)
            bars = entity.Bars.MapToResponse();
        if (entity.ProductPriceTables is not null)
            prices = entity.ProductPriceTables.MapToResponse();
        return new ProductResponse(entity.Id, entity.InternalCode, entity.Name, entity.Price, bars,
            entity.LaboratoryId, entity.ApplyPriceRule, prices);
    }

    public static IEnumerable<ProductResponse> MapToResponse(this IEnumerable<Product> entities)
    {
        return entities.Select(MapToResponse).ToList();
    }

    public static BarResponse MapToResponse(this Bar entity)
    {
        return new BarResponse(entity.Id, entity.ProductId, entity.Code);
    }

    public static ProductPriceTableResponse MapToResponse(this ProductPriceTable entity)
    {
        return new ProductPriceTableResponse(entity.Id, entity.ProductId, entity.MinimalAmount, entity.MaximumAmount,
            entity.Price);
    }

    public static IEnumerable<ProductPriceTableResponse> MapToResponse(this IEnumerable<ProductPriceTable> entities)
    {
        return entities.Select(MapToResponse);
    }

    public static IEnumerable<BarResponse> MapToResponse(this IEnumerable<Bar>? entities)
    {
        return entities is null ? [] : entities.Select(MapToResponse).ToList();
    }

    #endregion
}