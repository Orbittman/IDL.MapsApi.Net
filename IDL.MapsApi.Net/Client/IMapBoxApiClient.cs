using System.Threading.Tasks;

namespace IDL.MapsApi.Net.Client
{
    public interface IMapBoxApiClient
    {
        Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request) where TResponse : class, new();
    }
} 