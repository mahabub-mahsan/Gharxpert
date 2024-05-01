using AutoMapper;
using GharXpertWeb.Models.Dto;

namespace GharXpertWeb
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

            CreateMap<ConstructionTypeDTO, ConstructionTypeCreateDTO>().ReverseMap();
            CreateMap<ConstructionTypeDTO, ConstructionTypeUpdateDTO>().ReverseMap();

            CreateMap<CustomerDTO, CustomerCreateDTO>().ReverseMap();
            CreateMap<CustomerDTO, CustomerUpdateDTO>().ReverseMap();

            CreateMap<ContactOurExpertDTO, ContactOurExpertCreateDTO>().ReverseMap();
            CreateMap<ContactOurExpertDTO, ContactOurExpertUpdateDTO>().ReverseMap();

            CreateMap<QuotationMasterDTO, QuotationMasterCreateDTO>().ReverseMap();
            CreateMap<QuotationMasterDTO, QuotationMasterUpdateDTO>().ReverseMap();

            

        }
    }
}
