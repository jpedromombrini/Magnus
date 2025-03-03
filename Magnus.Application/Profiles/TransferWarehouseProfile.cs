using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class TransferWarehouseProfile : Profile
{
    public TransferWarehouseProfile()
    {
        CreateMap<TransferWarehouse, CreateTransferWarehouseRequest>().ReverseMap();
        CreateMap<TransferWarehouse, UpdateTransferWarehouseRequest>().ReverseMap();
        CreateMap<TransferWarehouse, TransferWarehouseResponse>().ReverseMap();
        CreateMap<TransferWarehouseItem, TransferWarehouseItemRequest>().ReverseMap();
        CreateMap<TransferWarehouseItem, TransferWarehouseItemResponse>()
            .ForMember(dest => dest.TransferWarehouseDestinyName,
                opt => opt.MapFrom(src => src.TransferWarehouse.WarehouseDestinyName))
            .ForMember(dest => dest.TransferWarehouseOriginName,
                opt => opt.MapFrom(src => src.TransferWarehouse.WarehouseOriginName))
            .ConstructUsing(src => new TransferWarehouseItemResponse(
                src.Id,
                src.ProductId,
                src.ProductInternalCode,
                src.ProductName,
                src.Amount,
                src.TransferWarehouse.WarehouseOriginName,
                src.TransferWarehouse.WarehouseDestinyName
                ))
            .ReverseMap();
    }
}