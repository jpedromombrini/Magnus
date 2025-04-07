using System.Text;
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

        CreateMap<SaleReceipt, CreateSaleReceiptRequest>().ReverseMap();
        CreateMap<SaleReceipt, UpdateSaleReceiptRequest>().ReverseMap();
        CreateMap<SaleReceipt, SaleReceiptResponse>().ReverseMap();

        CreateMap<SaleReceiptInstallmentRequest, SaleReceiptInstallment>()
            .ForMember(dest => dest.ProofImage, opt => opt.MapFrom(src => Convert.FromBase64String(src.ProofImage)));
            
        CreateMap<SaleReceiptInstallment, SaleReceiptInstallmentResponse>().ReverseMap();
    }
}