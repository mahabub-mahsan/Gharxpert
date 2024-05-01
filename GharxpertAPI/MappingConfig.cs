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

            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CustomerCreateDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();

            CreateMap<ConstructionType, ConstructionTypeDTO>().ReverseMap();
            CreateMap<ConstructionType, ConstructionTypeCreateDTO>().ReverseMap();
            CreateMap<ConstructionType, ConstructionTypeUpdateDTO>().ReverseMap();

            CreateMap<QuotationMaster, QuotationMasterDTO>().ReverseMap();
            CreateMap<QuotationMaster, QuotationMasterCreateDTO>().ReverseMap();
            CreateMap<QuotationMaster, QuotationMasterUpdateDTO>().ReverseMap();

            CreateMap<QuotationDetail, QuotationDetailDTO>().ReverseMap();
            CreateMap<QuotationDetail, QuotationDetailCreateDTO>().ReverseMap();
            CreateMap<QuotationDetail, QuotationDetailUpdateDTO>().ReverseMap();


            CreateMap<QuotationStatus, QuotationStatusDTO>().ReverseMap();
            CreateMap<QuotationStatus, QuotationStatusCreateDTO>().ReverseMap();
            CreateMap<QuotationStatus, QuotationStatusUpdateDTO>().ReverseMap();
            
        }
    }
}
