namespace Magnus.Application.Dtos.Filters;

public record SaleBySaleReportFilter(
    DateOnly InitialDate,
    DateOnly FinalDate);