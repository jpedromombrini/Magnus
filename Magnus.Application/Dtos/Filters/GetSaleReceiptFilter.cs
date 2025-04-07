namespace Magnus.Application.Dtos.Filters;

public record GetSaleReceiptFilter(
    DateOnly InitialDate,
    DateOnly FinalDate,
    int Document,
    Guid ClientId,
    Guid UserId);
