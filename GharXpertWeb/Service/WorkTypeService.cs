using Gharxpert_Utility;
using GharXpertWeb.Models;
using GharXpertWeb.Models.Dto;
using GharXpertWeb.Service.IService;

namespace GharXpertWeb.Service
{
    public class WorkTypeService : BaseService, IWorkTypeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string gXpertUrl;

        public WorkTypeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = httpClient;
            gXpertUrl = configuration.GetValue<string>("ServiceUrls:GharXpertAPI");
        }

        public Task<T> CreateAsync<T>(WorkTypeCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = gXpertUrl + "/api/workTypeAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = gXpertUrl + "/api/workTypeAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gXpertUrl + "/api/workTypeAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gXpertUrl + "/api/workTypeAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(WorkTypeUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = gXpertUrl + "/api/workTypeAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
