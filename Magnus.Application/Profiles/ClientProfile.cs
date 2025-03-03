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
        CreateMap<CreateClientRequest, Client>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? new Email(src.Email) : null))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => new Document(src.Document)))
            .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.Occupation))
            .ForMember(dest => dest.DateOfBirth,
                opt => opt.MapFrom(src => src.DateOfBirth != DateOnly.MinValue ? src.DateOfBirth : default))
            .ForMember(dest => dest.RegisterNumber, opt => opt.MapFrom(src => src.RegisterNumber))
            .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.SocialMedias))
            .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                !string.IsNullOrEmpty(src.ZipCode) || !string.IsNullOrEmpty(src.PublicPlace) || src.Number > 0 ||
                !string.IsNullOrEmpty(src.Neighborhood) || !string.IsNullOrEmpty(src.City) ||
                !string.IsNullOrEmpty(src.State) || !string.IsNullOrEmpty(src.Complement)
                    ? new Address(
                        src.ZipCode,
                        src.PublicPlace,
                        src.Number,
                        src.Neighborhood,
                        src.City,
                        src.State,
                        src.Complement)
                    : null))
            .ReverseMap();
        CreateMap<Client, UpdateClientRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email.Address : null))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document.Value))
            .ForMember(dest => dest.ZipCode,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.ZipCode : null))
            .ForMember(dest => dest.PublicPlace,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.PublicPlace : null))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address != null ? src.Address.Number : 0))
            .ForMember(dest => dest.Neighborhood,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.Neighborhood : null))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address != null ? src.Address.City : null))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address != null ? src.Address.State : null))
            .ForMember(dest => dest.Complement,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.Complement : null))
            .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => MapPhones(src.Phones)))
            .ReverseMap();
        CreateMap<Client, ClientResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email.Address : null))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document.Value))
            .ForMember(dest => dest.ZipCode,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.ZipCode : null))
            .ForMember(dest => dest.PublicPlace,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.PublicPlace : null))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address != null ? src.Address.Number : 0))
            .ForMember(dest => dest.Neighborhood,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.Neighborhood : null))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address != null ? src.Address.City : null))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address != null ? src.Address.State : null))
            .ForMember(dest => dest.Complement,
                opt => opt.MapFrom(src => src.Address != null ? src.Address.Complement : null))
            .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => MapPhones(src.Phones)))
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
        return socialMedias?.Select(sm => new ClientSocialMediaResponse(sm.Name, sm.Link)).ToList() ??
               [];
    }

    private List<ClientPhoneResponse> MapPhones(List<ClientPhone>? phones)
    {
        return phones?.Select(p => new ClientPhoneResponse(p.Phone.Number, p.Description)).ToList() ??
               [];
    }
}