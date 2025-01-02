using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Profiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<CreateSupplierRequest, Supplier>()
            .ConstructUsing(src => new Supplier(
                src.Name,
                new Document(src.Document),    
                new Email(src.Email),          
                new Phone(src.Phone),          
                new Address(src.ZipCode, src.PublicPlace, src.Number, src.Neighborhood, src.City, src.State, src.Complement)
            ))
            .ReverseMap();
       
        CreateMap<UpdateSupplierRequest, Supplier>()
            .ConstructUsing(src => new Supplier(
                src.Name,
                new Document(src.Document),    
                new Email(src.Email),          
                new Phone(src.Phone),          
                new Address(src.ZipCode, src.PublicPlace, src.Number, src.Neighborhood, src.City, src.State, src.Complement)
            ))
            .ReverseMap();
        CreateMap<Supplier, SupplierResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))  
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document.Value)) 
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Number))      
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))     
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
            .ForMember(dest => dest.PublicPlace, opt => opt.MapFrom(src => src.Address.PublicPlace))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Address.Neighborhood))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
            .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Address.Complement))
            .ConstructUsing(src => new SupplierResponse(
                src.Id,
                src.Name,
                src.Document.Value,
                src.Phone.Number,
                src.Email.Address,
                src.Address.ZipCode,
                src.Address.PublicPlace,
                src.Address.Number,
                src.Address.Neighborhood,
                src.Address.City,
                src.Address.State,
                src.Address.Complement
            ))
            .ReverseMap();
        
    }
}