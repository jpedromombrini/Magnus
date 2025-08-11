namespace Magnus.Application.Dtos.Filters;

public record GetCampaingnFilter(
    DateOnly InitialDate,
    DateOnly FinalDate,
    bool? Active
);