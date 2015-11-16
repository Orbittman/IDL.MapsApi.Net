using System.Threading.Tasks;

using IDL.MapsApi.Net.Client;

namespace IDL.MapsApi.Net
{
    public class MapBoxGeoLocationProvider : IGeoLocationProvider
    {
        private readonly IMapBoxApiClient _client;

        public MapBoxGeoLocationProvider(IMapBoxApiClient client = null)
        {
            _client = client ?? new MapBoxClient();
        }

        public Task<TResponse> GetAsync<TResponse>(IRequest<TResponse> request)
            where TResponse : class, new() 
        {
            return _client.GetAsync(request);
        }
    }
}
