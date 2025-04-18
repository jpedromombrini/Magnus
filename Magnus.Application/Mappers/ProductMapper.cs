using System.Security.Claims;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class ProductMapper
{
    #region Request

    public static Product MapToEntity(this CreateProductRequest request)
    {
        List<Bar> bars = [];
        List<ProductPriceTable> prices = [];
        Product product = new(request.Name, request.Price, request.LaboratoryId);
        if (request.Bars is not null) bars.AddRange(request.Bars.Select(bar => new Bar(product.Id, bar.Code)));
        if (request.ProductPriceTable is not null)
            prices.AddRange(request.ProductPriceTable.Select(productPrice =>
                new ProductPriceTable(product.Id, productPrice.MinimalAmount,
                    productPrice.MaximumAmount,
                    productPrice.Price)));
        foreach (var bar in bars) product.AddBar(bar);
        foreach (var priceTable in prices) product.AddProductPriceTable(priceTable);
        return product;
    }

    public static Product MapToEntity(this UpdateProductRequest request)
    {
        List<Bar> bars = [];
        List<ProductPriceTable> prices = [];
        Product product = new(request.Name, request.Price, request.LaboratoryId);
        if (request.Bars is not null) bars.AddRange(request.Bars.Select(bar => new Bar(product.Id, bar.Code)));
        if (request.ProductPriceTable is not null)
            prices.AddRange(request.ProductPriceTable.Select(productPrice =>
                new ProductPriceTable(product.Id, productPrice.MinimalAmount,
                    productPrice.MaximumAmount,
                    productPrice.Price)));
        foreach (var bar in bars) product.AddBar(bar);
        foreach (var priceTable in prices) product.AddProductPriceTable(priceTable);
        return product;
    }

    public static IEnumerable<Product> MapToEntity(this IEnumerable<CreateProductRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    public static IEnumerable<Product> MapToEntity(this IEnumerable<UpdateProductRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    #endregion

    #region Response

    public static ProductResponse MapToResponse(this Product product)
    {
        List<BarResponse> bars = [];
        List<ProductPriceTableResponse> prices = [];
        if (product.Bars is not null) bars.AddRange(product.Bars.Select(bar => new BarResponse(bar.Id, bar.Code)));
        if (product.ProductPriceTables is not null)
            prices.AddRange(product.ProductPriceTables.Select(price =>
                new ProductPriceTableResponse(price.Id, price.ProductId, price.MinimalAmount, price.MaximumAmount,
                    price.Price)));
        return new ProductResponse(product.Id, product.InternalCode, product.Name, product.Price, bars,
            product.LaboratoryId, prices);
    }

    public static IEnumerable<ProductResponse> MapToResponse(this IEnumerable<Product> requests)
    {
        return requests.Select(MapToResponse).ToList();
    }

    #endregion
}