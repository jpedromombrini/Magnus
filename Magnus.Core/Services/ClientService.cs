using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class ClientService(
    IUnitOfWork unitOfWork) : IClientService
{
    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
    }
}