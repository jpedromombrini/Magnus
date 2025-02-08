namespace Magnus.Application.Dtos.Requests;

public record SaleItemRequest(
    Guid ProductId,
    string ProductName,
    int Amount,
    decimal Value,
    decimal TotalPrice,
    decimal Discount,
    DateOnly Validity
);