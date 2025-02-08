using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class EstimateProfile : Profile
{
    public EstimateProfile()
    {
        CreateMap<Estimate, CreateEstimateRequest>().ReverseMap();
        CreateMap<Estimate, UpdateEstimateRequest>().ReverseMap();
        CreateMap<Estimate, EstimateResponse>()
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientName)) 
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)) 
            .ReverseMap();
        CreateMap<EstimateItem, EstimateItemRequest>().ReverseMap();
        CreateMap<EstimateItem, EstimateItemResponse>().ReverseMap();
    }
}