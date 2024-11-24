using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class ReceiptProfile : Profile
{
    public ReceiptProfile()
    {
        CreateMap<Receipt, CreateReceiptRequest>().ReverseMap();
        CreateMap<Receipt, UpdateReceiptRequest>().ReverseMap();
        CreateMap<Receipt, ReceiptResponse>().ReverseMap();
    }
}