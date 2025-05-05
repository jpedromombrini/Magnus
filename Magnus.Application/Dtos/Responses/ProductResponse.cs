namespace Magnus.Application.Dtos.Responses;

public record ProductResponse(
    Guid Id,
    int InternalCode,
    string Name,
    decimal Price,
    IEnumerable<BarResponse>? Bars,
    Guid LaboratoryId,
    IEnumerable<ProductPriceTableResponse>? ProductPriceTable);