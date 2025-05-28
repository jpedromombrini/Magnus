namespace Magnus.Application.Dtos.Responses;

public record AppConfigurationResponse(
    Guid Id,
    string CostCenterSale,
    int AmountToDiscount,
    int DaysValidityEstimate);