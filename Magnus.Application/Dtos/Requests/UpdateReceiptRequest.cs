namespace Magnus.Application.Dtos.Requests;

public record UpdateReceiptRequest(string Name, decimal Increase, bool InInstallments);