namespace Magnus.Application.Dtos.Responses;

public record ProductResponse(
    Guid Id,
    int InternalCode,
    string Name,
    decimal Price,
    PriceRuleResponse? PriceRule,
    List<BarResponse> Bars,
    Guid LaboratoryId);