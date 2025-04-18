namespace Magnus.Application.Dtos.Requests;

public record UpdateProductRequest( 
    string Code,
    int InternalCode,
    string Name,
    decimal Price,
    List<BarRequest>? Bars,
    Guid LaboratoryId,
    List<ProductPriceTableRequest>? ProductPriceTable);