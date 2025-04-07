using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class ReceiptService(IUnitOfWork unitOfWork) : IReceiptService
{
    public async Task<Receipt?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await unitOfWork.Receipts.GetByIdAsync(id, cancellationToken);
    }
}