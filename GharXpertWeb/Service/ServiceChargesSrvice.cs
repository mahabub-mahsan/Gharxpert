using Gharxpert_Utility;
using GharXpertWeb.Models;
using GharXpertWeb.Models.Dto;
using GharXpertWeb.Service.IService;

namespace GharXpertWeb.Service
{
    public class ServiceChargesSrvice : BaseService, IServiceChargesService
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly string gXpertUrl;

        public ServiceChargesSrvice(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = httpClient;
            gXpertUrl = configuration.GetValue<string>("ServiceUrls:GharXpertAPI");
        }

        public Task<T> CreateAsync<T>(ServiceChargesCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = gXpertUrl + "/api/serviceChargesAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = gXpertUrl + "/api/serviceChargesAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gXpertUrl + "/api/serviceChargesAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gXpertUrl + "/api/serviceChargesAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ServiceChargesUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = gXpertUrl + "/api/serviceChargesAPI/" + dto.Id,
                Token = token
            });
        }
    }
}

