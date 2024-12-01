using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class CostCenterRepository(MagnusContext context) : Repository<CostCenter>(context), ICostCenterRepository
{
    
}