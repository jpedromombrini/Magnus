using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Mappers;

public static class AddressMapper
{
    #region Request

    public static Address MapToEntity(this AddressRequest request)
    {
        return new Address(request.ZipCode, request.PublicPlace, request.Number, request.Neighborhood, request.City,
            request.State, request.Complement);
    }

    #endregion

    #region Response

    public static AddressResponse MapToResponse(this Address entity)
    {
        return new AddressResponse(entity.ZipCode, entity.PublicPlace, entity.Number, entity.Neighborhood, entity.City,
            entity.State, entity.Complement);
    }

    #endregion
}