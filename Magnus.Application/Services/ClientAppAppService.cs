using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;

namespace Magnus.Application.Services;

public class ClientAppAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IClientAppService
{
    public async Task AddClientAsync(CreateClientRequest request, CancellationToken cancellationToken)
    {
        await unitOfWork.Clients.AddAsync(mapper.Map<Client>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateClientAsync(Guid id, UpdateClientRequest request, CancellationToken cancellationToken)
    {
        var clientDb = await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (clientDb is null) throw new EntityNotFoundException(id);
        var clientReq = mapper.Map<Client>(request);
        clientDb.SetName(clientReq.Name);
        clientDb.SetDocument(clientReq.Document);
        if (clientReq.Address is not null) clientDb.SetAddress(clientReq.Address);
        if (clientReq.RegisterNumber is not null) clientDb.SetRegisterNumber(clientReq.RegisterNumber);
        if (clientReq.Email is not null) clientDb.SetEmail(clientReq.Email);
        if (clientReq.Occupation is not null) clientDb.SetOccupation(clientReq.Occupation);
        if (clientReq.DateOfBirth != DateOnly.MinValue) clientDb.SetDateOfBirth(clientReq.DateOfBirth);
        if (clientReq.Phones is not null) clientDb.SetPhones(clientReq.Phones);
        if (clientReq.SocialMedias is not null) clientDb.SetSocialMedias(clientReq.SocialMedias);
        
        unitOfWork.Clients.Update(clientDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ClientResponse>> GetClientsAsync(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<ClientResponse>>(await unitOfWork.Clients.GetAllAsync(cancellationToken));
    }

    public async Task<IEnumerable<ClientResponse>> GetClientsByFilterAsync(Expression<Func<Client, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<ClientResponse>>(
            await unitOfWork.Clients.GetAllByExpressionAsync(predicate, cancellationToken));
    }

    public async Task<ClientResponse> GetClientByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return mapper.Map<ClientResponse>(await unitOfWork.Clients.GetByIdAsync(id, cancellationToken));
    }

    public async Task DeleteClientAsync(Guid id, CancellationToken cancellationToken)
    {
        var clientDb = await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (clientDb is null) throw new EntityNotFoundException(id);
        unitOfWork.Clients.Delete(clientDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}