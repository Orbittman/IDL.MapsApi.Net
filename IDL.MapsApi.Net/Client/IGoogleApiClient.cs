using System.Threading.Tasks;

namespace IDL.MapsApi.Net.Client
{
    public interface IGoogleApiClient
    {
        Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request) where TResponse : class, new();
    }
}