using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface ISupplierAppService
{
    Task AddSupplierAsync(CreateSupplierRequest request, CancellationToken cancellationToken);
    Task UpdateSupplierAsync(Guid id, UpdateSupplierRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<SupplierResponse>> GetSuppliersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<SupplierResponse>> GetSuppliersByFilterAsync(Expression<Func<Supplier, bool>> predicate, CancellationToken cancellationToken);
    Task<SupplierResponse> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteSupplierAsync(Guid id, CancellationToken cancellationToken);
}