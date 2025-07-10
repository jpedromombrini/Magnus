using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;

public record GetEstimatesFilter(
    DateTime InitialDate,
    DateTime FinalDate,
    Guid ClientId,
    Guid UserId,
    string? Description,
    int Code,
    EstimateStatus Status);