namespace Magnus.Application.Dtos.Responses;

public record AppConfigurationResponse(
    Guid Id,
    CostCenterResponse? CostCenterSale,
    int AmountToDiscount,
    int DaysValidityEstimate);