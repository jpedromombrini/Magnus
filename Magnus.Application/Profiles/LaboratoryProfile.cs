using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class LaboratoryProfile : Profile
{
    public LaboratoryProfile()
    {
        CreateMap<Laboratory, LaboratoryResponse>().ReverseMap();
        CreateMap<Laboratory, CreateLaboratoryRequest>().ReverseMap();
        
    }
}