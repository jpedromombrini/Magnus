using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface IWarehouseAppService
{
    Task AddWarehouseAsync(CreateWarehouseRequest request, CancellationToken cancellationToken);
    Task UpdateWarehouseAsync(Guid id, UpdateWarehouseRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<WarehouseResponse>> GetWarehousesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<WarehouseResponse>> ListWarehousesByFilterAsync(Expression<Func<Warehouse, bool>> predicate, CancellationToken cancellationToken);
    Task<WarehouseResponse> GetWarehousesByFilterAsync(Expression<Func<Warehouse, bool>> predicate, CancellationToken cancellationToken);
    Task<WarehouseResponse> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteWarehouseAsync(Guid id, CancellationToken cancellationToken);
}