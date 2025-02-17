using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Bar, BarResponse>().ReverseMap();
        CreateMap<BarRequest, Bar>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<CreateProductRequest, Product>().ReverseMap();
    }
}