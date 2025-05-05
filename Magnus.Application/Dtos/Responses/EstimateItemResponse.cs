namespace Magnus.Application.Dtos.Responses;

public record EstimateItemResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Amount,
    decimal TotalValue,
    decimal Value,
    decimal Discount
);