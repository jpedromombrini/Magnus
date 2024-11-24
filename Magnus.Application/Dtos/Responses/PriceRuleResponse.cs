namespace Magnus.Application.Dtos.Responses;

public record PriceRuleResponse(Guid Id, Guid ProductId, int From, decimal Price, bool Active);