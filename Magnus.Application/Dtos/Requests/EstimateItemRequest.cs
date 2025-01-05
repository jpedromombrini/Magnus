namespace Magnus.Application.Dtos.Requests;

public record EstimateItemRequest(
    Guid ProductId,
    string ProductName,
    int Amount,
    decimal TotalValue,
    decimal Discount,
    decimal Value,
    Guid EstimateId
);