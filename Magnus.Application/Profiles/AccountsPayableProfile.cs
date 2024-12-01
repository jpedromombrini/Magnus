using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class AccountsPayableProfile : Profile
{
    public AccountsPayableProfile()
    {
        CreateMap<AccountsPayable, AccountsPayableRequest>().ReverseMap();
        CreateMap<AccountsPayable, AccountsPayableResponse>().ReverseMap();
        CreateMap<AccountsPayableOccurrence, AccountsPayableOccurrenceRequest>().ReverseMap();
        CreateMap<AccountsPayableOccurrence, AccountsPayableOccurenceResponse>().ReverseMap();
    }
}