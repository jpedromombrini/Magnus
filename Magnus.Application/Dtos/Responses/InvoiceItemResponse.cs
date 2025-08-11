namespace Magnus.Application.Dtos.Responses;

public record InvoiceItemResponse(
    Guid Id,
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    int Amount,
    decimal TotalValue,
    DateOnly Validity,
    string Lot);