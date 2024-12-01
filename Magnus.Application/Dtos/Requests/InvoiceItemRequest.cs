namespace Magnus.Application.Dtos.Requests;

public record InvoiceItemRequest(
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    decimal Amount,
    decimal TotalValue,
    DateOnly Validity,
    bool Bonus
);