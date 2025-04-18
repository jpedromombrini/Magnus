using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class BarRepository(MagnusContext context) : Repository<Bar>(context), IBarRepository
{
    private readonly MagnusContext _context = context;
    public async Task AddRangeAsync(IEnumerable<Bar> bars, CancellationToken cancellationToken)
    {
        await _context.Bars.AddRangeAsync(bars, cancellationToken);
    }

    public void RemoveRange(IEnumerable<Bar> bars)
    {
        _context.Bars.RemoveRange(bars);
    }
}