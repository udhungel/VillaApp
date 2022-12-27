using System.Net;

namespace MagicVilla_WebApp.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessage { get; set; }

        public bool IsSucess { get; set; } = true;

        public object Result { get; set; }
    }
}
