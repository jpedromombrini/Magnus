namespace Magnus.Application.Dtos.Responses;

public record ProductPriceTableResponse(
    Guid Id,
    Guid ProductId,
    int MinimalAmount,
    int MaximumAmount,
    decimal Price);
