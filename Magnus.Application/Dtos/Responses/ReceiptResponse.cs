namespace Magnus.Application.Dtos.Responses;

public record ReceiptResponse(
    Guid Id,
    string Name,
    decimal Increase,
    bool InInstallments);