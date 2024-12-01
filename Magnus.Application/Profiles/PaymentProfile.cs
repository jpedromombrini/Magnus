using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, CreatePaymentRequest>().ReverseMap();
        CreateMap<Payment, UpdatePaymentRequest>().ReverseMap();
        CreateMap<Payment, PaymentResponse>().ReverseMap();
        
    }
}