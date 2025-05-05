using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class SupplierAppService(
    IUnitOfWork unitOfWork,
    ISupplierService supplierService) : ISupplierAppService
{
    public async Task AddSupplierAsync(CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        await supplierService.AddSupplierAsync(request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateSupplierAsync(Guid id, UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        await supplierService.UpdateSupplierAsync(id, request.MapToEntity(), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SupplierResponse>> GetSuppliersAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Suppliers.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<SupplierResponse>> GetSuppliersByFilterAsync(
        Expression<Func<Supplier, bool>> predicate, CancellationToken cancellationToken)
    {
        return (await unitOfWork.Suppliers.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<SupplierResponse> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplier = await unitOfWork.Suppliers.GetByIdAsync(id, cancellationToken);
        if (supplier == null)
            throw new EntityNotFoundException("Fornecedor não encontrado com esse id");
        return (supplier).MapToResponse();
    }

    public async Task DeleteSupplierAsync(Guid id, CancellationToken cancellationToken)
    {
        var supplierDb = await unitOfWork.Suppliers.GetByIdAsync(id, cancellationToken);
        if (supplierDb is null)
            throw new EntityNotFoundException("Fornecedor não encontrado com esse id");
        unitOfWork.Suppliers.Delete(supplierDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}