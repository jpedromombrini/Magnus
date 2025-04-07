using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface IClientService
{
    Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}