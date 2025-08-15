using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class ClientService(
    IUnitOfWork unitOfWork) : IClientService
{
    public async Task<Client> ValidateClientAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (client is null)
            throw new EntityNotFoundException("cliente não encontrado com esse Id");
        return client;
    }
}