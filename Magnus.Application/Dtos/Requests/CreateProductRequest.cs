namespace Magnus.Application.Dtos.Requests;

public record CreateProductRequest(
    string Code,    
    string Name,
    decimal Price,
    decimal Discount,
    List<BarRequest>? Bars,
    PriceRuleRequest? PriceRule,
    Guid LaboratoryId
);
