using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;

public record AccountsReceivableFilter(
    DateOnly? InitialDueDate,
    DateOnly? FinalDueDate,
    DateTime? InitialEntryDate,
    DateTime? FinalEntryDate,
    DateTime? InitialReceiptDate,
    DateTime? FinalReceiptDate,
    Guid? ClientId,
    Guid? ReceiptId,
    int Document,
    AccountsReceivableStatus Status);