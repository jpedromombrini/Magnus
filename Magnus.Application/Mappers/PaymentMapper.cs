using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Mappers;

public static class PaymentMapper
{
    #region Request

    public static Payment MapToEntity(this CreatePaymentRequest request)
    {
        return new Payment(request.Name);
    }
    public static Payment MapToEntity(this UpdatePaymentRequest request)
    {
        return new Payment(request.Name);
    }
    

    #endregion
    #region Response

    public static PaymentResponse MapToResponse(this Payment entity)
    {
        return new PaymentResponse(entity.Id, entity.Name);
    }
    
    public static IEnumerable<PaymentResponse> MapToResponse(this IEnumerable<Payment> entities)
    {
        return entities.Select(MapToResponse);
    }
    

    #endregion
}