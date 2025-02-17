using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class AppConfigurationProfile : Profile
{
    public AppConfigurationProfile()
    {
        CreateMap<AppConfiguration, CreateAppConfigurationRequest>().ReverseMap();
        CreateMap<AppConfiguration, UpdateAppConfigurationRequest>().ReverseMap();
        CreateMap<AppConfiguration, AppConfigurationResponse>().ReverseMap();
    }
}