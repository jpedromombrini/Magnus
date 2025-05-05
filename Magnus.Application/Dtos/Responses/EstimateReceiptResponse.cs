namespace Magnus.Application.Dtos.Responses;

public record EstimateReceiptResponse(
    Guid Id,
    Guid? ClienteId,
    Guid UserId,
    Guid EstimateId,
    Guid ReceiptId,
    IEnumerable<EstimateReceiptInstallmentResponse> Installments);