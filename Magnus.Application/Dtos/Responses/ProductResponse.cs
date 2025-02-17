namespace Magnus.Application.Dtos.Responses;

public record ProductResponse(
    Guid Id,
    int InternalCode,
    string Name,
    decimal Price,
    decimal Discount,
    List<BarResponse> Bars,
    Guid LaboratoryId);