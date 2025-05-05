namespace Magnus.Application.Dtos.Responses;

public record SalesBySellerResponse(
    string SellerName,
    decimal TotalSale,
    decimal TotalFinantialDiscount);
