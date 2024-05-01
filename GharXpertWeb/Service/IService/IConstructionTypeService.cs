using GharXpertWeb.Models.Dto;

namespace GharXpertWeb.Service.IService
{
    public interface IConstructionTypeService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ConstructionTypeCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ConstructionTypeUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
