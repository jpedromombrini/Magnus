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
        CreateMap<TransferWarehouseItem, TransferWarehouseItemResponse>().ReverseMap();
    }
}