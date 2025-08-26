namespace Magnus.Application.Dtos.Responses;

public record SaleByProductResponse(
    ProductResponse Product,
    int Amount,
    decimal TotalSale,
    IEnumerable<SaleByProductSellerResponse> SaleByProductSeller);

public record SaleByProductSellerResponse(
    Guid SellerId,
    string SellerName,
    int Amount,
    decimal TotalSale);