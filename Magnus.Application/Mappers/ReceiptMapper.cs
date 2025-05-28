using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class ReceiptMapper
{
    #region Request

    public static Receipt MapToEntity(this CreateReceiptRequest request)
    {
        return new Receipt(request.Name, request.Increase, request.InInstallments);
    }

    public static Receipt MapToEntity(this UpdateReceiptRequest request)
    {
        return new Receipt(request.Name, request.Increase, request.InInstallments);
    }

    #endregion

    #region Response

    public static ReceiptResponse MapToResponse(this Receipt entity)
    {
        return new ReceiptResponse(entity.Id, entity.Name, entity.Increase, entity.InIstallments);
    }

    public static IEnumerable<ReceiptResponse> MapToResponse(this IEnumerable<Receipt> entities)
    {
        return entities.Select(MapToResponse);
    }

    #endregion
}