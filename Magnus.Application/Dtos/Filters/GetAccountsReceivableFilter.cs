using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;

public record GetAccountsReceivableFilter(
    DateOnly InitialDueDate,
    DateOnly FinalDueDate,
    Guid? ClientId,
    int Number,
    AccountsReceivableStatus Status);
