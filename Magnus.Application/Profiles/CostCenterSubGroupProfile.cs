using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class CostCenterSubGroupProfile : Profile
{
    public CostCenterSubGroupProfile()
    {
        CreateMap<CostCenterSubGroup, CreateCostCenterSubGroupRequest>().ReverseMap();
        CreateMap<CostCenterSubGroup, UpdateCostCenterSubGroupRequest>().ReverseMap();
        CreateMap<CostCenterSubGroup, CostCenterSubGroupResponse>().ReverseMap();
    }
}