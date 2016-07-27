using System.Threading.Tasks;

namespace IDL.MapsApi.Net
{
    public interface IMappingDataProvider
    {
        Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request)
            where TResponse : class, new();
    }
}
