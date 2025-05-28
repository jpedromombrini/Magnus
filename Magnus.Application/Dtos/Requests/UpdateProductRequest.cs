namespace Magnus.Application.Dtos.Requests;

public record UpdateProductRequest(
    string Code,
    int InternalCode,
    string Name,
    decimal Price,
    IEnumerable<BarRequest>? Bars,
    Guid LaboratoryId,
    bool ApplyPriceRule,
    IEnumerable<ProductPriceTableRequest>? ProductPriceTable);