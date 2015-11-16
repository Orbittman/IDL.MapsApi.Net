using System.Net.Http;
using System.Threading.Tasks;

namespace IDL.MapsApi.Net.Client
{
    public interface IApiClient
    {
        HttpMessageHandler ResponseHandler { get; set; }

        Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request) where TResponse : class, new();
    }
}