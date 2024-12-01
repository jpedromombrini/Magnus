using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class CostCenterProfile : Profile
{
    public CostCenterProfile()
    {
        CreateMap<CostCenter, CreateCostCenterRequest>().ReverseMap();
        CreateMap<CostCenter, UpdateCostCenterRequest>().ReverseMap();
        CreateMap<CostCenter, CostCenterResponse>().ReverseMap();
        
    }
}