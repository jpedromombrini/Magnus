namespace Magnus.Application.Dtos.Responses;

public record SaleItemResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Amount,
    decimal Value,
    decimal TotalPrice,
    decimal Discount,
    DateOnly Validity);