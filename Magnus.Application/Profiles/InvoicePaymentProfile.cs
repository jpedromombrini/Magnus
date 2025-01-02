using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class InvoicePaymentProfile : Profile
{
    public InvoicePaymentProfile()
    {
        CreateMap<InvoicePayment, InvoicePaymentRequest>().ReverseMap();
        CreateMap<InvoicePayment, InvoicePaymentResponse>().ReverseMap();
        CreateMap<InvoicePaymentInstallment, InvoicePaymentInstallmentRequest>().ReverseMap();
        CreateMap<InvoicePaymentInstallment, InvoicePaymentInstallmentResponse>().ReverseMap();
    }
}