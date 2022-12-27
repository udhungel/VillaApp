using MagicVilla_WebApp.Models;
using MagicVilla_WebApp.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using VillaApp_Utility;

namespace MagicVilla_WebApp.Services
{
    public class BaseService : IBaseService
    {
        // we are implementing a generic base service 
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient) 
        {
           this.responseModel = new APIResponse();
           this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                // create new HTTP message
                // Header type : application/json
                // URL : Url in apiRequest.Url 
                // Data : check null and serialize the data using Json Convert this is only possible if apiRequest is not null 
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data !=null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.APiType.POST: 
                        message.Method = HttpMethod.Post;
                        break;
                        case SD.APiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                        case SD.APiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                        default:
                        message.Method = HttpMethod.Get; 
                        break;           
                }

                HttpResponseMessage apiResponse = null; //response we get set it to null for now by default 

                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync(); // extract the content 
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent); // deserialize then we get APIResponse model from the VillaApp_Web 
                return APIResponse; 


            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessage = new List<string> { Convert.ToString(ex.Message) },
                    IsSucess = false
                };
                //have to serialize & deserilize to the generic type T , directly returning dto will not work it needs to be type T
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;

               
            }
        }
    }
}
