using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class CostCenterGroupProfile : Profile
{
    public CostCenterGroupProfile()
    {
        CreateMap<CostCenterGroup, CreateCostCenterGroupRequest>().ReverseMap();
        CreateMap<CostCenterGroup, UpdateCostCenterGroupRequest>().ReverseMap();
        CreateMap<CostCenterGroup, CostCenterGroupResponse>().ReverseMap();
    }
}