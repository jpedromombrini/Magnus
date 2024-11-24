namespace Magnus.Application.Dtos.Responses;

public record TransferWarehouseItemResponse(
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    decimal Amount,
    DateOnly Validity);