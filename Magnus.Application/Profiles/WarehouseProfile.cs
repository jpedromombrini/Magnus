using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class WarehouseProfile : Profile
{
    public WarehouseProfile()
    {
        CreateMap<CreateWarehouseRequest, Warehouse>().ReverseMap();
        CreateMap<UpdateWarehouseRequest, Warehouse>().ReverseMap();
        CreateMap<Warehouse, WarehouseResponse>();
    }
}