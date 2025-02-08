namespace Magnus.Application.Dtos.Requests;

public record UpdateProductRequest( 
    string Code,
    int InternalCode,
    string Name,
    decimal Price,
    decimal Discount,
    List<BarRequest>? Bars,
    PriceRuleRequest PriceRule,
    Guid LaboratoryId);