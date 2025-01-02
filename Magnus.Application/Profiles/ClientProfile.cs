using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<Client, CreateClientRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document.Value))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(dest => dest.PublicPlace, opt => opt.MapFrom(src => src.Address.PublicPlace))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Address.Neighborhood))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Address.Complement))
            .ReverseMap();
        CreateMap<Client, UpdateClientRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document.Value))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(dest => dest.PublicPlace, opt => opt.MapFrom(src => src.Address.PublicPlace))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Address.Neighborhood))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Address.Complement))
            .ReverseMap();
        CreateMap<Client, ClientResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document.Value))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(dest => dest.PublicPlace, opt => opt.MapFrom(src => src.Address.PublicPlace))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Address.Neighborhood))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Address.Complement))
            .ConstructUsing(src => new ClientResponse(
                src.Id,
                src.Name,
                MapEmail(src.Email),
                src.Document.Value,
                src.Occupation,
                src.DateOfBirth,
                MapAddressField(src.Address != null ? src.Address.ZipCode : null),
                MapAddressField(src.Address != null ? src.Address.PublicPlace : null),
                src.Address != null ? src.Address.Number : 0,
                MapAddressField(src.Address != null ? src.Address.Neighborhood : null),
                MapAddressField(src.Address != null ? src.Address.City : null),
                MapAddressField(src.Address != null ? src.Address.State : null),
                MapAddressField(src.Address != null ? src.Address.Complement : null),
                src.RegisterNumber,
                MapSocialMedias(src.SocialMedias),
                MapPhones(src.Phones)
            ))
            .ReverseMap();
    }
    private string? MapEmail(Email? email)
    {
        return email?.Address;
    }

    private string MapAddressField(string? field)
    {
        return field ?? string.Empty;  
    }

    private List<ClientSocialMediaResponse> MapSocialMedias(List<ClientSocialMedia>? socialMedias)
    {
        return socialMedias?.Select(sm => new ClientSocialMediaResponse(sm.Name, sm.Link)).ToList() ?? new List<ClientSocialMediaResponse>();
    }

    private List<ClientPhoneResponse> MapPhones(List<ClientPhone>? phones)
    {
        return phones?.Select(p => new ClientPhoneResponse(p.Phone.Number, p.Description)).ToList() ?? new List<ClientPhoneResponse>();
    }
}