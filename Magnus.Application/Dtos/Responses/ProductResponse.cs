namespace Magnus.Application.Dtos.Responses;

public record ProductResponse(
    Guid Id,
    int InternalCode,
    string Name,
    decimal Price,
    IEnumerable<BarResponse>? Bars,
    Guid LaboratoryId,
    bool ApplyPriceRule,
    IEnumerable<ProductPriceTableResponse>? ProductPriceTable,
    Guid? ProductGroupId,
    string? ProductGroupResponse);