using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services;

public interface IReceiptAppService
{
    Task AddReceiptAsync(CreateReceiptRequest request, CancellationToken cancellationToken);
    Task UpdateReceiptAsync(Guid id, UpdateReceiptRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<ReceiptResponse>> GetReceiptsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ReceiptResponse>> GetReceiptsByFilterAsync(Expression<Func<Receipt, bool>> predicate, CancellationToken cancellationToken);
    Task<ReceiptResponse> GetReceiptByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteReceiptAsync(Guid id, CancellationToken cancellationToken);
}