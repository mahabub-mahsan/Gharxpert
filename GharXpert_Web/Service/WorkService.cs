using Gharxpert_Utility;
using GharXpert_Web.Models;
using GharXpert_Web.Models.Dto;
using GharXpert_Web.Service.IService;

namespace GharXpert_Web.Service
{
    public class WorkService : BaseService, IWorkService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string gharxpertUrl;
        public WorkService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = httpClient;
            gharxpertUrl = configuration.GetValue<string>("ServiceUrls:GharXpertAPI");
        }
        public Task<T> CreateAsync<T>(WorkCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = gharxpertUrl + "/api/workAPI",
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = gharxpertUrl + "/api/workAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gharxpertUrl + "/api/workAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = gharxpertUrl + "/api/workAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(WorkUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = gharxpertUrl + "/api/workAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
