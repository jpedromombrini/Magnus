namespace Magnus.Application.Dtos.Responses;

public record SaleBySellerResponse(
    SellerResponse Seller,
    IEnumerable<SaleResponse> Sales);