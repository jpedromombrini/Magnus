using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<Sale, CreateSaleRequest>().ReverseMap();
        CreateMap<Sale, UpdateSaleRequest>().ReverseMap();
        CreateMap<Sale, SaleResponse>().ReverseMap();

        CreateMap<SaleItem, SaleItemRequest>().ReverseMap();
        CreateMap<SaleItem, SaleItemResponse>().ReverseMap();
        
        CreateMap<SaleReceipt, SaleReceiptRequest>().ReverseMap();
        CreateMap<SaleReceipt, SaleReceiptResponse>().ReverseMap();
        
        CreateMap<SaleReceiptInstallment, SaleReceiptInstallmentRequest>().ReverseMap();
        CreateMap<SaleReceiptInstallment, SaleReceiptInstallmentResponse>().ReverseMap();
        
    }
}