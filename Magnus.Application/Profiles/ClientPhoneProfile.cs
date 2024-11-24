using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class ClientPhoneProfile : Profile
{
    public ClientPhoneProfile()
    {
        CreateMap<ClientPhone, ClientPhoneRequest>().ReverseMap();
        CreateMap<ClientPhone, ClientPhoneResponse>().ReverseMap();
    }
}