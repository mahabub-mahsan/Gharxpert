using AutoMapper;
using GharXpert_Web.Models.Dto;

namespace GharXpert_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<WorkDTO, WorkCreateDTO>().ReverseMap();
            CreateMap<WorkDTO, WorkUpdateDTO>().ReverseMap();

            CreateMap<WorkTypeDTO, WorkTypeCreateDTO>().ReverseMap();
            CreateMap<WorkTypeDTO, WorkTypeUpdateDTO>().ReverseMap();

            CreateMap<ServiceChargesDTO, ServiceChargesCreateDTO>().ReverseMap();
            CreateMap<ServiceChargesDTO, ServiceChargesUpdateDTO>().ReverseMap();

            CreateMap<ContactOurExpertDTO, ContactOurExpertCreateDTO>().ReverseMap();
            CreateMap<ContactOurExpertDTO, ContactOurExpertUpdateDTO>().ReverseMap();
        }
    }
}
