using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Mappers;

public static class SellerMapper
{
    #region Request

    public static Seller MapToEntity(this CreateSellerRequest request)
    {
        return new Seller(request.Name, new Phone(request.Phone), new Email(request.Email));
    }

    public static Seller MapToEntity(this UpdateSellerRequest request)
    {
        return new Seller(request.Name, new Phone(request.Phone), new Email(request.Email));
    }

    public static IEnumerable<Seller> MapToEntity(this IEnumerable<CreateSellerRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    public static IEnumerable<Seller> MapToEntity(this IEnumerable<UpdateSellerRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    #endregion

    #region Response

    public static SellerResponse MapToResponse(this Seller entity)
    {
        return new SellerResponse(entity.Id, entity.Name, entity.Document?.Value, entity.Phone.Number,
            entity.Email.Address, entity.UserId);
    }

    public static IEnumerable<SellerResponse> MapToResponse(this IEnumerable<Seller> entity)
    {
        return entity.Select(MapToResponse).ToList();
    }

    #endregion
}