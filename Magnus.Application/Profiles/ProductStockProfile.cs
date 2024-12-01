using AutoMapper;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class ProductStockProfile : Profile
{
    public ProductStockProfile()
    {
        CreateMap<ProductStock, ProductStockResponse>().ReverseMap();
    }
}