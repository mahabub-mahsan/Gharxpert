using GharXpertWeb.Models.Dto;

namespace GharXpertWeb.Service.IService
{
    public interface IWorkService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(WorkCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(WorkUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
