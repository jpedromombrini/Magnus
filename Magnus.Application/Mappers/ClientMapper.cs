using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Mappers;

public static class ClientMapper
{
    #region Response

    public static ClientResponse MapToResponse(this Client entity)
    {
        return new ClientResponse(entity.Id, entity.Name, entity.Email?.Address, entity.Document.Value,
            entity.Occupation, entity.DateOfBirth, entity.Address?.MapToResponse(), entity.RegisterNumber,
            entity.SocialMedias?.MapToResponse(), entity.Phones?.MapToResponse());
    }

    public static IEnumerable<ClientResponse> MapToResponse(this IEnumerable<Client> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion

    #region Request

    public static Client MapToEntity(this CreateClientRequest request)
    {
        var client = new Client(request.Name, new Document(request.Document));
        if (!string.IsNullOrWhiteSpace(request.Email))
            client.SetEmail(new Email(request.Email));
        client.SetDateOfBirth(request.DateOfBirth);
        if (request.Address is not null)
            client.SetAddress(request.Address.MapToEntity());
        client.SetOccupation(request.Occupation);
        client.SetRegisterNumber(request.RegisterNumber);
        if (request.Phones is not null)
            foreach (var phone in request.Phones)
                client.AddPhone(phone.MapToEntity());

        if (request.SocialMedias is null) return client;
        foreach (var socialMedia in request.SocialMedias)
            client.AddSocialMedia(socialMedia.MapToEntity());
        return client;
    }

    public static Client MapToEntity(this UpdateClientRequest request)
    {
        var client = new Client(request.Name, new Document(request.Document));
        if (!string.IsNullOrWhiteSpace(request.Email))
            client.SetEmail(new Email(request.Email));
        client.SetDateOfBirth(request.DateOfBirth);
        if (request.Address is not null)
            client.SetAddress(request.Address.MapToEntity());
        client.SetOccupation(request.Occupation);
        client.SetRegisterNumber(request.RegisterNumber);
        if (request.Phones is not null)
            foreach (var phone in request.Phones)
                client.AddPhone(phone.MapToEntity());

        if (request.SocialMedias is null) return client;
        foreach (var socialMedia in request.SocialMedias)
            client.AddSocialMedia(socialMedia.MapToEntity());
        return client;
    }

    public static IEnumerable<Client> MapToEntity(this IEnumerable<CreateClientRequest> requests)
    {
        return requests.Select(MapToEntity);
    }

    #endregion
}