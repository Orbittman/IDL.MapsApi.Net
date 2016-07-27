using System.Threading.Tasks;

using IDL.MapsApi.Net.Client;

namespace IDL.MapsApi.Net
{
    public class MappingDataProvider : IMappingDataProvider
    {
        private readonly IApiClient _client;

        public MappingDataProvider(IApiClient client = null)
        {
            _client = client ?? new ApiClient();
        }

        public Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request)
            where TResponse : class, new()
        {
            return _client.GetAsync(request);
        }
    }
}
