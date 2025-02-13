﻿using GharXpertWeb.Models.Dto;

namespace GharXpertWeb.Service.IService
{
    public interface IServiceChargesService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ServiceChargesCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ServiceChargesUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
