using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Mappers;

public static class SupplierMapper
{
    #region Request

    public static Supplier MapToEntity(this CreateSupplierRequest request)
    {
        var supplier = new Supplier(request.Name, new Document(request.Document));
        if (!string.IsNullOrEmpty(request.Phone))
            supplier.SetPhone(new Phone(request.Phone));
        if (!string.IsNullOrEmpty(request.Email))
            supplier.SetEmail(new Email(request.Email));
        if (!string.IsNullOrEmpty(request.ZipCode))
            supplier.SetAddress(new Address(request.ZipCode, request.PublicPlace, request.Number, request.Neighborhood,
                request.City, request.State, request.Complement));

        return supplier;
    }

    public static Supplier MapToEntity(this UpdateSupplierRequest request)
    {
        var supplier = new Supplier(request.Name, new Document(request.Document));
        if (!string.IsNullOrEmpty(request.Phone))
            supplier.SetPhone(new Phone(request.Phone));
        if (!string.IsNullOrEmpty(request.Email))
            supplier.SetEmail(new Email(request.Email));
        if (!string.IsNullOrEmpty(request.ZipCode))
            supplier.SetAddress(new Address(request.ZipCode, request.PublicPlace, request.Number, request.Neighborhood,
                request.City, request.State, request.Complement));

        return supplier;
    }

    public static IEnumerable<Supplier> MapToEntity(
        this IEnumerable<CreateSupplierRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    public static IEnumerable<Supplier> MapToEntity(
        this IEnumerable<UpdateSupplierRequest> requests)
    {
        return requests.Select(MapToEntity).ToList();
    }

    #endregion

    #region Response

    public static SupplierResponse MapToResponse(this Supplier entity)
    {
        return new SupplierResponse(entity.Id, entity.Name, entity.Document.Value, entity.Phone?.Number,
            entity.Email?.Address, entity.Address?.ZipCode, entity.Address?.PublicPlace, entity.Address?.Number ?? 0,
            entity.Address?.Neighborhood, entity.Address?.City, entity.Address?.State, entity.Address?.Complement);
    }

    public static IEnumerable<SupplierResponse> MapToResponse(this IEnumerable<Supplier> entity)
    {
        return entity.Select(MapToResponse).ToList();
    }

    #endregion
}