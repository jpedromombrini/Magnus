using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class CostCenterSubGroupRepository(MagnusContext context)
    : Repository<CostCenterSubGroup>(context), ICostCenterSubGroupRepository
{
}