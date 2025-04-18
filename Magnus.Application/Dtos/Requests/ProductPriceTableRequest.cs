namespace Magnus.Application.Dtos.Requests;

public record ProductPriceTableRequest(
    Guid Id,
    Guid ProductId,
    int MinimalAmount,
    int MaximumAmount,
    decimal Price);