namespace Magnus.Application.Dtos.Requests;

public record TransferWarehouseItemRequest(
    Guid ProductId,
    int ProductInternalCode,
    string ProductName,
    decimal Amount,
    DateOnly Validity);