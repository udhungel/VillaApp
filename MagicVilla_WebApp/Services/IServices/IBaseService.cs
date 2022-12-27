using MagicVilla_WebApp.Models;

namespace MagicVilla_WebApp.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync <T>(APIRequest apiRequest);// every time we call we pass a Api Request Model and return type will be generic 
    }
}
