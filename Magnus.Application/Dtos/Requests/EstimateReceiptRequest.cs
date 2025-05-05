namespace Magnus.Application.Dtos.Requests;

public record EstimateReceiptRequest(
    Guid? ClienteId,
    Guid UserId,
    Guid EstimateId,
    Guid ReceiptId,
    IEnumerable<EstimateReceiptInstallmentRequest> Installments);