using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IClientService
{
    Task<Client> ValidateClientAsync(Guid id, CancellationToken cancellationToken);
}