namespace Magnus.Application.Dtos.Requests;

public record CreateProductRequest(
    string Code,    
    string Name,
    decimal Price,
    IEnumerable<BarRequest>? Bars,
    Guid LaboratoryId,
    IEnumerable<ProductPriceTableRequest>? ProductPriceTable);
