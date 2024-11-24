namespace Magnus.Application.Dtos.Requests;

public record PriceRuleRequest(
    int From,
    decimal Price,
    bool Active);