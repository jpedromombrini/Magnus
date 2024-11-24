namespace Magnus.Application.Dtos.Requests;

public record CreateReceiptRequest(string Name, decimal Increase, bool InInstallments);