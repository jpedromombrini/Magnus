namespace Magnus.Application.Services.Interfaces;

public interface IAuditProductAppService
{
    Task<int> GetBalanceAsync(Guid productId, int warwarehouseId, CancellationToken cancellationToken); 
}