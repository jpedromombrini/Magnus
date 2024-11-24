using AutoMapper;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Core.Entities;

namespace Magnus.Application.Profiles;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, CreateDoctorRequest>().ReverseMap();
        CreateMap<Doctor, UpdateDoctorRequest>().ReverseMap();
        CreateMap<Doctor, DoctorResponse>().ReverseMap();
    }
}