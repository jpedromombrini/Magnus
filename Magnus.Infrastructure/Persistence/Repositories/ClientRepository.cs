using System.Linq.Expressions;
using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class ClientRepository(MagnusContext context) : Repository<Client>(context), IClientRepository
{
    private readonly MagnusContext _context = context;

    public override async Task<IEnumerable<Client>> GetAllByExpressionAsync(Expression<Func<Client, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Clients
            .AsNoTracking()
            .Where(predicate)
            .Include(x => x.Phones)
            .Include(x => x.SocialMedias)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Clients
            .AsNoTracking()
            .Include(x => x.Phones)
            .Include(x => x.SocialMedias)
            .ToListAsync(cancellationToken);
    }

    public override Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Clients
            .Include(x => x.Phones)
            .Include(x => x.SocialMedias)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            
    }
};