using static VillaApp_Utility.SD;

namespace MagicVilla_WebApp.Models
{
    public class APIRequest
    {
        public APiType ApiType { get; set; } = APiType.GET;

        public string Url { get; set; }

        public object Data { get; set; }
    }
}
