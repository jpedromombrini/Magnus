namespace Magnus.Application.Dtos.Requests;

public record InvoiceItemRequest(
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    int Amount,
    decimal TotalValue,
    DateOnly Validate,
    string Lot);