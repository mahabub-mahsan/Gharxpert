using GharXpertWeb.Models.Dto;

namespace GharXpertWeb.Service.IService
{
    public interface IContactOurExpertService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ContactOurExpertCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ContactOurExpertUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
