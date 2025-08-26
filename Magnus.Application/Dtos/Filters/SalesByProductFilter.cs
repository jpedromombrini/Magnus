namespace Magnus.Application.Dtos.Filters;

public record SalesByProductFilter(DateOnly InitialDate, DateOnly FinalDate);