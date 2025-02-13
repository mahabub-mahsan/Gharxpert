﻿using GharXpertWeb.Models.Dto;

namespace GharXpertWeb.Service.IService
{
    public interface IWorkTypeService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(WorkTypeCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(WorkTypeUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
