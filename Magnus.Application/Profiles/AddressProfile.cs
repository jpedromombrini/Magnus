using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.ValueObjects;

namespace Magnus.Application.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressRequest>().ReverseMap();
        CreateMap<Address, AddressResponse>().ReverseMap(); 
    }
}