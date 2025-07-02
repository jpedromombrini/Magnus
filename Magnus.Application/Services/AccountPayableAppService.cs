using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class AccountPayableAppService(
    IAccountPayableService accountPayableService,
    IUnitOfWork unitOfWork) : IAccountPayableAppService
{
    public async Task AddAccountPayableAsync(CreateAccountsPayableRequest request, CancellationToken cancellationToken)
    {
        var entity = request.MapToEntity();
        await accountPayableService.CreateAsync(entity, cancellationToken);
        await unitOfWork.AccountsPayables.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAccountPayableAsync(Guid id, UpdateAccountsPayableRequest request,
        CancellationToken cancellationToken)
    {
        var entityExists = await unitOfWork.AccountsPayables.GetByIdAsync(id, cancellationToken);
        if (entityExists is null)
            throw new EntityNotFoundException("nenhum contas a pagar encontrado com esse id");
        var entity = request.MapToEntity();
        await accountPayableService.UpdateAsync(entityExists, entity, cancellationToken);
        unitOfWork.AccountsPayables.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<AccountsPayableResponse> GetAccountPayableByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.AccountsPayables.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new EntityNotFoundException("nenhum contas a pagar encontrado com esse id");

        return entity.MapToResponse();
    }

    public async Task DeleteAccountPayableAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.AccountsPayables.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new EntityNotFoundException("nenhum contas a pagar encontrado com esse id");
        unitOfWork.AccountsPayables.Delete(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<AccountsPayableResponse>> GetAccountPayablesByFilterAsync(
        GetAccountsPayableFilter filter, CancellationToken cancellationToken)
    {
        var accounts = await unitOfWork.AccountsPayables.GetAllByExpressionAsync(
            x => (filter.SupplierId == null || x.SupplierId == filter.SupplierId) &&
                 (filter.Document == 0 || x.Document == filter.Document) &&
                 (filter.LaboratoryId == null || filter.LaboratoryId == x.LaboratoryId) &&
                 (filter.PaymentId == null || filter.PaymentId == x.PaymentId) &&
                 (filter.InitialDueDate == null || x.DueDate >= filter.InitialDueDate) &&
                 (filter.FinalDueDate == null || x.DueDate <= filter.FinalDueDate) &&
                 (filter.InitialEntryDate == null || x.CreatedAt >= filter.InitialEntryDate) &&
                 (filter.FinalEntryDate == null || x.CreatedAt <= filter.FinalEntryDate) &&
                 (filter.Status == AccountPayableStatus.All || filter.Status == x.AccountPayableStatus)
            , cancellationToken);
        return accounts.MapToResponse();
    }
}