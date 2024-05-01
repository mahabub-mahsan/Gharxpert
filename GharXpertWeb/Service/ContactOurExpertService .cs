﻿using Gharxpert_Utility;
using GharXpertWeb.Models;
using GharXpertWeb.Models.Dto;
using GharXpertWeb.Service.IService;

namespace GharXpertWeb.Service
{
    public class ContactOurExpertService : BaseService, IContactOurExpertService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string gXpertUrl;

        public ContactOurExpertService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = httpClient;
            gXpertUrl = configuration.GetValue<string>("ServiceUrls:GharXpertAPI");
        }

        public Task<T> CreateAsync<T>(ContactOurExpertCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = gXpertUrl + "/api/contactOurExpertAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = gXpertUrl + "/api/contactOurExpertAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gXpertUrl + "/api/contactOurExpertAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gXpertUrl + "/api/contactOurExpertAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ContactOurExpertUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = gXpertUrl + "/api/contactOurExpertAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
