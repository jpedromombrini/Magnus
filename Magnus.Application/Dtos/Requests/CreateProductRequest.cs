namespace Magnus.Application.Dtos.Requests;

public record CreateProductRequest(
    string Code,    
    string Name,
    decimal Price,
    List<BarRequest>? Bars,
    Guid LaboratoryId,
    List<ProductPriceTableRequest>? ProductPriceTable);
