using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;

namespace GharxpertAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Work, WorkDTO>();
            CreateMap<WorkDTO, Work>();

            CreateMap<Work, WorkCreateDTO>().ReverseMap();
            CreateMap<Work, WorkUpdateDTO>().ReverseMap();

            CreateMap<WorkType, WorkTypeDTO>().ReverseMap();
            CreateMap<WorkType, WorkTypeCreateDTO>().ReverseMap();
            CreateMap<WorkType, WorkTypeUpdateDTO>().ReverseMap();

            CreateMap<ServiceCharges, ServiceChargesDTO>().ReverseMap();
            CreateMap<ServiceCharges, ServiceChargesCreateDTO>().ReverseMap();
            CreateMap<ServiceCharges, ServiceChargesUpdateDTO>().ReverseMap();

            CreateMap<ContactOurExpert, ContactOurExpertDTO>().ReverseMap();
            CreateMap<ContactOurExpert, ContactOurExpertCreateDTO>().ReverseMap();
            CreateMap<ContactOurExpert, ContactOurExpertUpdateDTO>().ReverseMap();
        }
    }
}
