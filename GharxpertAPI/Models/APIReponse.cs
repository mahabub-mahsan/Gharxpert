using System.Net;

namespace GharxpertAPI.Models
{
    public class APIReponse
    {
        public APIReponse()
        {
            ErrorMessages = new List<string>();   
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
