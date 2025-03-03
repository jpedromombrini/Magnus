using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Services.Interfaces;

public interface IClientAppService
{
    Task AddClientAsync(CreateClientRequest request, CancellationToken cancellationToken);
    Task UpdateClientAsync(Guid id, UpdateClientRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<ClientResponse>> GetClientsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ClientResponse>> GetClientsByFilterAsync(Expression<Func<Client, bool>> predicate, CancellationToken cancellationToken);
    Task<ClientResponse> GetClientByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteClientAsync(Guid id, CancellationToken cancellationToken);
}