using System.Net.Http;

namespace IDL.MapsApi.Net.Client
{
    public interface IApiClient
    {
        HttpMessageHandler ResponseHandler { get; set; }
    }
}