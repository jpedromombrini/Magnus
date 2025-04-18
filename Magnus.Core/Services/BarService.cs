using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class BarService(IUnitOfWork unitOfWork) : IBarService
{
    public async Task AddRangeAsync(IEnumerable<Bar> bars, CancellationToken cancellationToken)
    {
        await unitOfWork.Bars.AddRangeAsync(bars, cancellationToken);
    }

    public async Task<IEnumerable<Bar>> GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        return await unitOfWork.Bars.GetAllByExpressionAsync(x => x.ProductId == productId, cancellationToken);
    }

    public async Task RemoveRangeAsync(Guid productId, CancellationToken cancellationToken)
    {
        var bars = await GetByProductId(productId, cancellationToken);
        unitOfWork.Bars.RemoveRange(bars);
    }
}