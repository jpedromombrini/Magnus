using System.Linq.Expressions;
using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Services;

public class ClientAppAppService(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IClientAppService
{
    public async Task AddClientAsync(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var clientDb = await unitOfWork.Clients.GetByExpressionAsync(x => x.Document.Value == request.Document, cancellationToken);
        if(clientDb is not null)
            throw new ApplicationException("Já existe um cliente com esse documento");
        clientDb = await unitOfWork.Clients.GetByExpressionAsync(x => x.Email.Address.ToLower() == request.Email.ToLower(), cancellationToken);
        if(clientDb is not null)
            throw new ApplicationException("Já existe um cliente com esse email");
        clientDb = await unitOfWork.Clients.GetByExpressionAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
        if(clientDb is not null)
            throw new ApplicationException("Já existe um cliente com esse nome");
        
        await unitOfWork.Clients.AddAsync(mapper.Map<Client>(request), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateClientAsync(Guid id, UpdateClientRequest request, CancellationToken cancellationToken)
    {
        var clientDb = await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (clientDb is null) throw new EntityNotFoundException(id);
        clientDb.SetName(request.Name);
        clientDb.SetDocument(new Document(request.Document));
        if (!string.IsNullOrEmpty(request.ZipCode))
        {
            Address address = new(request.ZipCode, request.PublicPlace!, request.Number, request.Neighborhood!,
                request.City!, request.State!, request.Complement!);
            clientDb.SetAddress(address);
        }
        
        if (!string.IsNullOrEmpty(request.RegisterNumber)) clientDb.SetRegisterNumber(request.RegisterNumber);
        if (request.Email is not null) clientDb.SetEmail(new Email(request.Email));
        if (!string.IsNullOrEmpty(request.Occupation)) clientDb.SetOccupation(request.Occupation);
        if (request.DateOfBirth != DateOnly.MinValue) clientDb.SetDateOfBirth(request.DateOfBirth);
        if (request.Phones is not null) clientDb.SetPhones(mapper.Map<List<ClientPhone>>(request.Phones));
        if (request.SocialMedias is not null)
            clientDb.SetSocialMedias(mapper.Map<List<ClientSocialMedia>>(request.SocialMedias));

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