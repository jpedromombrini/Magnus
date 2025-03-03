using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface ITransferWarehouseAppService
{
    Task AddTransferWarehouseAsync(CreateTransferWarehouseRequest request, CancellationToken cancellationToken);
    Task UpdateTransferWarehouseAsync(Guid id, UpdateTransferWarehouseRequest request, CancellationToken cancellationToken);
    Task UpdateTransferWarehouseItemStatusAsync(UpdateStatusTransferWarehouseItemRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<TransferWarehouseResponse>> GetTransferWarehousesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TransferWarehouseResponse>> GetTransferWarehouseByFilterAsync(Expression<Func<TransferWarehouse, bool>> predicate, CancellationToken cancellationToken);
    Task<IEnumerable<TransferWarehouseItemResponse>> GetTransferWarehouseItemsByFilterAsync(Expression<Func<TransferWarehouseItem, bool>> predicate, CancellationToken cancellationToken);
    Task<TransferWarehouseResponse> GetTransferWarehouseByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteTransferWarehouseAsync(Guid id, CancellationToken cancellationToken);
}