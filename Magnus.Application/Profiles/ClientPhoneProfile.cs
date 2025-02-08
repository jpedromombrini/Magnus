using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Profiles;

public class ClientPhoneProfile : Profile
{
    public ClientPhoneProfile()
    {
        CreateMap<ClientPhoneRequest, ClientPhone>()
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => new Phone(src.Number)))
            .ReverseMap();
        CreateMap<ClientPhone, ClientPhoneResponse>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Phone.Number))
            .ReverseMap();
    }
}