using System.Linq.Expressions;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Services;

public class ClientAppAppService(
    IUnitOfWork unitOfWork) : IClientAppService
{
    public async Task AddClientAsync(CreateClientRequest request, CancellationToken cancellationToken)
    {
        var clientDb =
            await unitOfWork.Clients.GetByExpressionAsync(x => x.Document.Value == request.Document, cancellationToken);
        if (clientDb is not null)
            throw new ApplicationException("Já existe um cliente com esse documento");
        clientDb = await unitOfWork.Clients.GetByExpressionAsync(
            x => x.Email != null && request.Email != null && x.Email.Address.ToLower() == request.Email.ToLower(),
            cancellationToken);
        if (clientDb is not null)
            throw new ApplicationException("Já existe um cliente com esse email");
        clientDb = await unitOfWork.Clients.GetByExpressionAsync(x => x.Name.ToLower() == request.Name.ToLower(),
            cancellationToken);
        if (clientDb is not null)
            throw new ApplicationException("Já existe um cliente com esse nome");
        Client client = new(request.Name, new Document(request.Document));
        if (!string.IsNullOrEmpty(request.RegisterNumber)) client.SetRegisterNumber(request.RegisterNumber);
        if (request.Email is not null) client.SetEmail(new Email(request.Email));
        if (!string.IsNullOrEmpty(request.Occupation)) client.SetOccupation(request.Occupation);
        if (request.DateOfBirth != DateOnly.MinValue) client.SetDateOfBirth(request.DateOfBirth);
        if (request.Address is not null)
            client.SetAddress(request.Address.MapToEntity());

        if (request.Phones is not null)
            foreach (var phone in request.Phones)
                client.AddPhone(phone.MapToEntity());

        if (request.SocialMedias is not null)
            foreach (var socialMedia in request.SocialMedias)
                client.AddSocialMedia(socialMedia.MapToEntity());
        await unitOfWork.Clients.AddAsync(client, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateClientAsync(Guid id, UpdateClientRequest request, CancellationToken cancellationToken)
    {
        var clientDb = await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (clientDb is null) throw new EntityNotFoundException(id);
        clientDb.SetName(request.Name);
        clientDb.SetDocument(new Document(request.Document));
        if (request.Address is not null)
            clientDb.SetAddress(request.Address.MapToEntity());

        if (!string.IsNullOrEmpty(request.RegisterNumber)) clientDb.SetRegisterNumber(request.RegisterNumber);
        if (request.Email is not null) clientDb.SetEmail(new Email(request.Email));
        if (!string.IsNullOrEmpty(request.Occupation)) clientDb.SetOccupation(request.Occupation);
        if (request.DateOfBirth != DateOnly.MinValue) clientDb.SetDateOfBirth(request.DateOfBirth);

        if (clientDb.Phones is not null)
        {
            var phonesToRemove = clientDb.Phones.Where(p =>
                request.Phones is not null && request.Phones.All(x => x.Number != p.Phone.Number));
            if (request.Phones is not null)
                foreach (var phone in request.Phones)
                {
                    var existingPhone = clientDb.Phones.SingleOrDefault(p => p.Phone.Number == phone.Number);
                    if (existingPhone != null)
                    {
                        existingPhone.SetPhone(new Phone(phone.Number));
                        existingPhone.SetDescription(phone.Description);
                    }
                    else
                    {
                        clientDb.AddPhone(phone.MapToEntity());
                    }
                }

            if (clientDb.SocialMedias is not null)
            {
                var socialToRemove = clientDb.SocialMedias.Where(s =>
                    request.SocialMedias is not null && request.SocialMedias.All(x => x.Name != s.Name));
                if (request.SocialMedias is not null)
                    foreach (var socialMedia in request.SocialMedias)
                    {
                        var existingSocialMedia =
                            clientDb.SocialMedias.SingleOrDefault(s => s.Name == socialMedia.Name);
                        if (existingSocialMedia is not null)
                        {
                            existingSocialMedia.SetName(socialMedia.Name);
                            existingSocialMedia.SetLink(socialMedia.Link);
                        }
                        else
                        {
                            clientDb.AddSocialMedia(socialMedia.MapToEntity());
                        }
                    }

                unitOfWork.Clients.DeletePhonesRange(phonesToRemove);
                unitOfWork.Clients.DeleteSocialMediasRange(socialToRemove);
            }
        }

        unitOfWork.Clients.Update(clientDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ClientResponse>> GetClientsAsync(CancellationToken cancellationToken)
    {
        return (await unitOfWork.Clients.GetAllAsync(cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<ClientResponse>> GetClientsByFilterAsync(Expression<Func<Client, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return (await unitOfWork.Clients.GetAllByExpressionAsync(predicate, cancellationToken)).MapToResponse();
    }

    public async Task<IEnumerable<ClientResponse>> GetClientsByDocumentAsync(string document,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(document))
            return (await unitOfWork.Clients.GetAllAsync(cancellationToken)).MapToResponse();
        var documentSanitized = document.Replace(".", "").Replace("/", "").Replace("-", "");
        return (await unitOfWork.Clients.GetAllByExpressionAsync(x => x.Document.Value.Contains(documentSanitized),
            cancellationToken)).MapToResponse();
    }

    public async Task<ClientResponse> GetClientByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (client is null)
            throw new EntityNotFoundException("Cliente não encontrado com esse Id");
        return client.MapToResponse();
    }

    public async Task DeleteClientAsync(Guid id, CancellationToken cancellationToken)
    {
        var clientDb = await unitOfWork.Clients.GetByIdAsync(id, cancellationToken);
        if (clientDb is null) throw new EntityNotFoundException(id);
        unitOfWork.Clients.Delete(clientDb);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}