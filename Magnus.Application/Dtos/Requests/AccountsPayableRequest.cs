namespace Magnus.Application.Dtos.Requests;

public record AccountsPayableRequest(
    DateTime DueDate,
    DateTime PaymentDate,
    decimal Value,
    decimal PaymentValue,
    decimal Discount,
    decimal Interest,    
    int Installment);