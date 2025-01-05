using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class InvoiceProfile : Profile
{
    public InvoiceProfile()
    {
        CreateMap<Invoice, CreateInvoiceRequest>().ReverseMap();
        CreateMap<Invoice, UpdateInvoiceRequest>().ReverseMap();
        CreateMap<Invoice, InvoiceResponse>().ReverseMap();
        CreateMap<InvoiceItem, InvoiceItemRequest>().ReverseMap();
        CreateMap<InvoiceItem, InvoiceItemResponse>().ReverseMap();
    }
}