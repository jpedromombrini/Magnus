using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Filters;

public record GetAccountsPayableFilter(
    DateOnly? InitialDueDate,
    DateOnly? FinalDueDate,
    DateTime? InitialEntryDate,
    DateTime? FinalEntryDate,
    Guid? LaboratoryId,
    Guid? SupplierId,
    Guid? PaymentId,
    int? Document,
    AccountPayableStatus Status);