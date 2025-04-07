using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IReceiptService
{
    Task<Receipt?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
}