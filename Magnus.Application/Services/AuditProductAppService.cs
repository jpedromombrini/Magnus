using Magnus.Application.Services.Interfaces;
using Magnus.Core.Enumerators;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class AuditProductAppService(IUnitOfWork unitOfWork) : IAuditProductAppService
{
    public async Task<int> GetBalanceAsync(Guid productId, int warwarehouseId, CancellationToken cancellationToken)
    {
        var inBalance = await unitOfWork.AuditProducts.GetBalanceAsync(productId, AuditProductType.In, warwarehouseId, cancellationToken);
        var outBalance = await unitOfWork.AuditProducts.GetBalanceAsync(productId, AuditProductType.Out, warwarehouseId, cancellationToken);
        return inBalance - outBalance;
    }
}