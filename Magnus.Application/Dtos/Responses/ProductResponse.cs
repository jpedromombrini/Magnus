namespace Magnus.Application.Dtos.Responses;

public record ProductResponse(
    Guid Id,
    int InternalCode,
    string Name,
    decimal Price,
    List<BarResponse> Bars,
    Guid LaboratoryId,
    List<ProductPriceTableResponse>? ProductPriceTable);