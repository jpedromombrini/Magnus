using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Services;

public class SupplierAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : ISupplierAppService
{
    public async Task AddSupplierAsync(CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.Suppliers.AddAsync(mapper.Map<Supplier>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSupplierAsync(Guid id, UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        var supplierDb = await unitOfWork.Suppliers.GetByIdAsync(id, cancellationToken);
        if (supplierDb is null)
            throw new EntityNotFoundException(id);
        supplierDb.SetName(request.Name);
        supplierDb.SetAddress(mapper.Map<Address>(request.Address));
        supplierDb.SetDocument(new Document(request.Document));
        supplierDb.SetEmail(new Email(request.Email));
        unitOfWork.Suppliers.Update(supplierDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SupplierResponse>> GetSuppliersAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SupplierResponse>>(await unitOfWork.Suppliers.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<SupplierResponse>> GetSuppliersByFilterAsync(
        Expression<Func<Supplier, bool>> predicate, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<SupplierResponse>>(
            await unitOfWork.Suppliers.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<SupplierResponse> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<SupplierResponse>(await unitOfWork.Suppliers.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteSupplierAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplierDb = await unitOfWork.Suppliers.GetByIdAsync(id, cancellationToken);
        if (supplierDb is null)
            throw new EntityNotFoundException(id);
        unitOfWork.Suppliers.Delete(supplierDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}