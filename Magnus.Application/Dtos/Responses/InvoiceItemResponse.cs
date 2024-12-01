namespace Magnus.Application.Dtos.Responses;

public record InvoiceItemResponse( Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    decimal Amount,
    decimal TotalValue,
    DateOnly Validity,
    bool Bonus);