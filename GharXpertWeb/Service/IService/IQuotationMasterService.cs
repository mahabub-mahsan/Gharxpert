using GharXpertWeb.Models.Dto;

namespace GharXpertWeb.Service.IService
{
    public interface IQuotationMasterService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(QuotationMasterCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(QuotationMasterUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
