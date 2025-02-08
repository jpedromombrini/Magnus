using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class SellerProfile : Profile
{
    public SellerProfile()
    {
        CreateMap<Seller, CreateSellerRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document != null ? src.Document.Value: null))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Number))
            .ReverseMap();
        CreateMap<Seller, UpdateSellerRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document != null ? src.Document.Value: null))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Number))
            .ReverseMap();
        CreateMap<Seller, SellerResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document != null ? src.Document.Value: null))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Number))
            .ReverseMap();
    }
}