using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class ClientMediaSocialProfile : Profile
{
    public ClientMediaSocialProfile()
    {
        CreateMap<ClientSocialMedia, ClientSocialMediaRequest>().ReverseMap();
        CreateMap<ClientSocialMedia, ClientSocialMediaResponse>().ReverseMap();
    }
}